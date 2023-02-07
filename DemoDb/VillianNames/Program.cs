using System;
using VillianNames.IO;
using System.Data.SqlClient;
using DemoDb;
using System.Text;

namespace VillianNames
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var queryReader = new QueryReader("querySQL");
            queryReader.GetOrCreatePath();
            queryReader.GetOrCreateFile();

            using var connection = new SqlConnection(Config.connectionString);
            connection.Open();

            var result = GetMinionCount(connection, queryReader);
            Console.WriteLine(result);

            connection.Close();
        }

        private static string GetMinionCount(SqlConnection connection, QueryReader query)
        {
            var sb = new StringBuilder();

            var command = new SqlCommand(query.ReadToEnd(), connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                string villainName = (string)reader["Name"];
                int minionsCount = (int)reader["Number of minions"];
                sb.AppendLine($"{villainName} - {minionsCount}");
            }
            return sb.ToString();
        }
    }
}
