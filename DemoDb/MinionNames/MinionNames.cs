using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinionNames
{
    public class Villains
    {
        public Villains(SqlConnection connection)
        {
            Connection = connection;
        }

        public SqlConnection Connection { get; }

        public string GetMinionNamesOfVillain(int villainId, string queryFileName)
        {
            var queryReader = new QueryReader($"{queryFileName}");
            queryReader.GetOrCreatePath();
            queryReader.GetOrCreateFile();

            var query = queryReader.ReadToEnd();
            var command = new SqlCommand(query, Connection);

            var parameter = new SqlParameter("@VillainId", villainId);
            command.Parameters.Add(parameter);

            var reader = command.ExecuteReader();
            var sb = new StringBuilder();
            var counter = 1;

            if (!reader.HasRows)
            {
                sb.AppendLine($"No villain with ID {villainId} exists in the database.");
            }

            while (reader.Read())
            {
                string VillainName = (string)reader["VillainName"];

                if (counter == 1)
                {
                    sb.AppendLine($"Villain: {VillainName}");
                }

                string minionName = (string)reader["MinionName"];

                if (counter == 1 && string.IsNullOrWhiteSpace(minionName))
                {
                    sb.AppendLine("(no minions)");
                    break;
                }

                int minionAge = (int)reader["MinionAge"];
                sb.AppendLine($"{counter}. {minionName} {minionAge}");
                counter++;
            }

            return sb.ToString();
        }


    }
}
