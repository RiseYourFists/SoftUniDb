using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MiniORM
{

	//Entity classes must be reference types which can be instantiated

	internal class ChangeTracker<TEntity>
		where TEntity : class, new()
	{
		//Load all entities available
		private readonly IList<TEntity> allEntities;

		//Added but not saved
		private readonly IList<TEntity> added;

		//Removed but still not saved
		private readonly IList<TEntity> removed;

		private ChangeTracker()
		{
			this.added = new List<TEntity>();
			this.removed = new List<TEntity>();
		}

		public ChangeTracker(IEnumerable<TEntity> enitites)
			:this()
		{
			this.allEntities = CloneEntities(enitites);
		}

		public IReadOnlyCollection<TEntity> AllEntities
		=> (IReadOnlyCollection<TEntity>)this.allEntities;

		public IReadOnlyCollection<TEntity> Added
			=> (IReadOnlyCollection<TEntity>)this.added;

		public IReadOnlyCollection<TEntity> Removed
			=> (IReadOnlyCollection<TEntity>)this.removed;

		private static IList<TEntity> CloneEntities(IEnumerable<TEntity> entities)
		{
			IList<TEntity> clonedEntities = new List<TEntity>();

			PropertyInfo[] propertiesToClone = typeof(TEntity)
				.GetProperties()
				.Where(pi => DBContext.AllowedSqlTypes.Contains(pi.PropertyType))
				.ToArray();

			foreach (TEntity originalEnitity in entities)
			{
				TEntity clonedEntity = Activator.CreateInstance<TEntity>();

				foreach (PropertyInfo  property in propertiesToClone)
				{
					object originalValue = property.GetValue(originalEnitity);
					property.SetValue(clonedEntity, originalValue);
				}

				clonedEntities.Add(clonedEntity);
			}
			return clonedEntities;
		}

		public void Add(TEntity entity)
			=> this.added.Add(entity);

		public void Remove(TEntity entity)
			=> this.removed.Add(entity);

		public IEnumerable<TEntity> GetModifiedEntities(DbSet<TEntity> dbSet)
		{
			IList<TEntity> modifiedEntities = new List<TEntity>();

			var primaryKeys = typeof(TEntity)
				.GetProperties()
				.Where(pi => pi.HasAttribute<KeyAttribute>())
				.ToArray();

			foreach (TEntity proxyEntity in this.AllEntities)
			{

				IEnumerable<object> proxyEntityPrimaryKeyValues = 
					GetPrimaryKeyValues(primaryKeys, proxyEntity)
					.ToArray();

				TEntity originalEntity = dbSet
					.Entities
					.Single(e => GetPrimaryKeyValues(primaryKeys, e)
						.SequenceEqual( proxyEntityPrimaryKeyValues));

				bool isModified = IsModified(originalEntity, proxyEntity);

				if (isModified )
				{
					modifiedEntities.Add(originalEntity);
				}
			}

			return modifiedEntities;
		}

		private static IEnumerable<object> GetPrimaryKeyValues(IEnumerable<PropertyInfo> primaryKeys, TEntity entity)
		=> primaryKeys.Select(pk => pk.GetValue(entity));

        private static bool IsModified(TEntity original, TEntity proxy)
		{
			IEnumerable<PropertyInfo> monitoredProperties = typeof(TEntity)
				.GetProperties()
				.Where(pi => DBContext.AllowedSqlTypes.Contains(pi.PropertyType));

			ICollection<PropertyInfo> modifiedProperties = monitoredProperties
				.Where(pi => !Equals(pi.GetValue(original), pi.GetValue(proxy)))
				.ToArray();

			return modifiedProperties.Any();
        }
    }
}