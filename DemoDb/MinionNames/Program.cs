using System;
using System.Data.SqlClient;
using System.Text;

namespace MinionNames
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var connection = new SqlConnection(Config.connectionString);
            connection.Open();

            var minionNames = new Villains(connection);
            Console.WriteLine(minionNames.GetMinionNamesOfVillain(1, "MinionNamesQuery"));


            connection.Close();
        }
    }
}
