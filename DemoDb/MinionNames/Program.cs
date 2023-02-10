using System;
using System.Data.SqlClient;

namespace MinionNames
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var connection = new SqlConnection(Config.connectionString);
            connection.Open();

            //var minionsOfVillain = new Villain(connection);
            //Console.WriteLine(minionsOfVillain.GetMinionNamesOfVillain(1, "MinionNamesQuery"));
            
            var minion = new Minion(connection);
            minion.AddMinion("Carry", 20, "Eindhoven", "Jimmy");

            connection.Close();
        }
    }
}
