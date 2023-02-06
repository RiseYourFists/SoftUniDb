using System;
using System.Data.SqlClient;

namespace DemoDb
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var connection = new SqlConnection(Config.connection);
            connection.Open();

            var query = "SELECT TOP(5) * FROM [Employees]";
            var selectCommand = new SqlCommand(query, connection);
            using var reader = selectCommand.ExecuteReader();

            while (reader.Read())
            {
                var firstName = reader["FirstName"];
                var middleName = reader["MiddleName"];
                var lastName = reader["LastName"];
                var jobTitle = reader["JobTitle"];
                var hireDate = reader["HireDate"];
                var salary = reader["Salary"];

                Console.WriteLine($"{firstName} {middleName} {lastName} {jobTitle} {hireDate} {salary}");
            }
            reader.Close();
            connection.Close();
        }
    }
}
