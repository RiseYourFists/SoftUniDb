SELECT 
	  [v].[Name] AS [VillainName]
	, [m].[Name] AS [MinionName]
	, [m].[Age] AS [MinionAge]

FROM [MinionsVillains] AS [mv]

JOIN [Villains] AS [v]
ON [v].[Id] = [mv].[VillainId]

JOIN [Minions] AS [m]
ON [m].[Id] = [mv].[MinionId]

WHERE [v].[Id] = @VillainId
ORDER BY [m].[Name]
