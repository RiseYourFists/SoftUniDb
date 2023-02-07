SELECT 
	  [v].[Name]
	, COUNT([m].[VillainId]) AS [Number of minions]
FROM [Villains] AS [v]
JOIN[MinionsVillains]  AS [m]
ON [m].[VillainId] = [v].[Id]
	GROUP BY [v].[Id],[v].[Name]
	HAVING COUNT([m].[MinionId]) > 3