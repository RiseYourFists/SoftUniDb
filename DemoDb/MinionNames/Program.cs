using System;
using System.Data.SqlClient;
using System.Text;

namespace MinionNames
{
    public class Program
    {
        static void Main(string[] args)
        {
            var queryReader = new QueryReader("MinionNamesQuery");
            queryReader.GetOrCreatePath();
            queryReader.GetOrCreateFile();

            using var connection = new SqlConnection(Config.connectionString);
            connection.Open();

            var query = queryReader.ReadToEnd();
            var command = new SqlCommand(query, connection);

            Console.Write("Pick villain number: ");
            var villainId = int.Parse(Console.ReadLine());

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

            Console.WriteLine(sb.ToString());

            connection.Close();
        }
    }
}
