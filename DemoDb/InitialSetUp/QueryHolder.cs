using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialSetUp
{
    public static class QueryHolder
    {
        public static string minionDbCreaterQuery = @"
CREATE TABLE [Countries](
  [Id] INT PRIMARY KEY IDENTITY
, [Name] NVARCHAR(40) NOT NULL
)

CREATE TABLE [Towns](
  [Id] INT PRIMARY KEY IDENTITY
, [Name] NVARCHAR(85) NOT NULL
, [CountryCode] INT FOREIGN KEY REFERENCES [Countries] NOT NULL
)

CREATE TABLE [Minions](
  [Id] INT PRIMARY KEY IDENTITY
, [Name] NVARCHAR(50) NOT NULL
, [Age] INT NOT NULL
, [TownId] INT FOREIGN KEY REFERENCES [Towns]
)

CREATE TABLE [EvilnessFactors](
  [Id] INT PRIMARY KEY IDENTITY
, [Name] VARCHAR(50) NOT NULL
)

CREATE TABLE [Villains](
  [Id] INT PRIMARY KEY IDENTITY
, [Name] NVARCHAR(50) NOT NULL
, [EvilnessFactorId] INT FOREIGN KEY REFERENCES [EvilnessFactors]
)

CREATE TABLE [MinionsVillains](
  [MinionId] INT FOREIGN KEY REFERENCES [Minions]
, [VillainId] INT FOREIGN KEY REFERENCES [Villains]
PRIMARY KEY([MinionId],[VillainId])
)

INSERT INTO [EvilnessFactors]
VALUES
  ('super good')
, ('good')
, ('bad')
, ('evil')
, ('super evil')

INSERT INTO [Villains]
VALUES
  ('Destructo', 5)
, ('Berserker', 4)
, ('Kaboom', 3)
, ('Edward', 2)
, ('Becca', 1)

INSERT INTO [Countries]
VALUES
  ('France')
, ('Belgium')
, ('England')
, ('Spain')
, ('Italy')

INSERT INTO [Towns]
VALUES
  ('Paris',1)
, ('Brussels', 2)
, ('London', 3)
, ('Madrid', 4)
, ('Rome', 5)

INSERT INTO [Minions]
VALUES
  ('Billy', 15, 1)
, ('Dingo', 15, 2)
, ('Conner', 15, 3)
, ('Steve', 15, 4)
, ('Bobby', 15, 5)

INSERT INTO [MinionsVillains]([MinionId],[VillainId])
VALUES
  (1, 1)
, (2, 2)
, (3, 3)
, (4, 4)
, (5, 5)";
    }
}
