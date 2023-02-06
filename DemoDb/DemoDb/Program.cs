using System;
using System.Data.SqlClient;

namespace DemoDb
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var connection = new SqlConnection(Config.connectionString);
            connection.Open();

            var qurey = @"SELECT TOP(5) 
                             [FirstName]
                            ,[LastName]
                            ,[JobTitle]
                            ,[Salary]
                          FROM [Employees]";

            var command = new SqlCommand(qurey, connection);
            using var reader = command.ExecuteReader();
            var counter = 1;
            while (reader.Read())
            {
                string firstName = (string)reader["FirstName"];
                string lastName = (string)reader["LastName"];
                string jobTitle = (string)reader["JobTitle"];
                decimal salary = (decimal)reader["Salary"];

                Console.WriteLine($"#{counter}. {firstName} {lastName}:\n     -Job: {jobTitle}\n     -Payment: {salary:f2}$");
                counter++;
            }
            reader.Close();

            connection.Close();
        }
    }
}
