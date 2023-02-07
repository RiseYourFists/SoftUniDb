using DemoDb;
using System;
using System.Data.SqlClient;

namespace InitialSetUp
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var connection = new SqlConnection(Config.connectionString);
            connection.Open();

            var command = new SqlCommand(QueryHolder.minionDbCreaterQuery, connection);
            command.ExecuteNonQuery();
            Console.WriteLine("Command has been executed.");

            connection.Close();
        }
    }
}
