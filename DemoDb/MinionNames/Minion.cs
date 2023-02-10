using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MinionNames
{
    public class Minion
    {
        public Minion(SqlConnection connection)
        {
            Connection = connection;
        }

        public SqlConnection Connection { get; }

        public void AddMinion(string name, int age, string townName, string villainName)
        {
            
            var queryReaders = new List<QueryReader>()
            {
                new QueryReader("SelectTownQuery"),
                new QueryReader("SelectMinionQuery"),
                new QueryReader("VillainSelectQuery"),
                new QueryReader("AddTownQuery"),
                new QueryReader("AddMinionQuery"),
                new QueryReader("AddVillainQuery"),
                new QueryReader("AddMinionToVillainQuery")
            };

            queryReaders.ForEach(x => x.InitializeReader());

            SqlTransaction transaction = Connection.BeginTransaction();

            try
            {
                var townQueryCommand = new SqlCommand(queryReaders[0].ReadToEnd(), Connection, transaction);
                townQueryCommand.Parameters.Add(new SqlParameter("@Town", townName));

                var addTownCommand = new SqlCommand(queryReaders[3].ReadToEnd(), Connection, transaction);
                addTownCommand.Parameters.Add(new SqlParameter("@Town", townName));

                var townId = townQueryCommand.ExecuteScalar();
                if (townId is null)
                {
                    addTownCommand.ExecuteNonQuery();
                    townId = townQueryCommand.ExecuteScalar();
                    Console.WriteLine($"Town {townName} was added to the database.");
                }

                var villainQueryCommand = new SqlCommand(queryReaders[2].ReadToEnd(), Connection, transaction);
                villainQueryCommand.Parameters.Add(new SqlParameter("@Name", villainName));

                var addVillainQuery = new SqlCommand(queryReaders[5].ReadToEnd(), Connection, transaction);
                addVillainQuery.Parameters.Add(new SqlParameter("@Name", villainName));

                var villainId = villainQueryCommand.ExecuteScalar();
                if (villainId is null)
                {
                    addVillainQuery.ExecuteNonQuery();
                    villainId = villainQueryCommand.ExecuteScalar();
                    Console.WriteLine($"Villain {villainName} was added to the database.");
                }

                var minionQueryCommand = new SqlCommand(queryReaders[1].ReadToEnd(), Connection, transaction);
                minionQueryCommand.Parameters.Add(new SqlParameter("@Name", name));

                var addMinionCommand = new SqlCommand(queryReaders[4].ReadToEnd(), Connection, transaction);
                addMinionCommand.Parameters.Add(new SqlParameter("@Name", name));
                addMinionCommand.Parameters.Add(new SqlParameter("@Age", age));
                addTownCommand.Parameters.Add(new SqlParameter("@TownId", townId));

                var minionId = minionQueryCommand.ExecuteScalar();
                if (minionId is null)
                {
                    addMinionCommand.ExecuteNonQuery();
                    minionId = minionQueryCommand.ExecuteScalar();
                }

                var linkMinionToVillain = new SqlCommand(queryReaders[6].ReadToEnd(), Connection, transaction);
                linkMinionToVillain.Parameters.Add(new SqlParameter("@VillainId", villainId));
                linkMinionToVillain.Parameters.Add(new SqlParameter("@MinionId", minionId));

                linkMinionToVillain.ExecuteNonQuery();
                Console.WriteLine($"Successfully added {name} to be minion of {villainName}");
                transaction.Commit();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
            }

        }
    }
}
