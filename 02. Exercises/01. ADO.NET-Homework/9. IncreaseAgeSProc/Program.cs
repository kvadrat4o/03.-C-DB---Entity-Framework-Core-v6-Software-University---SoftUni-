using System;
using System.Data.SqlClient;

namespace _9.Increase_Age_Stored_Procedure
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection
                (
                "Server=.;Database=MinionsDB;Integrated Security=True"
                );

            connection.Open();
            using (connection)
            {
                var id = int.Parse(Console.ReadLine());

                var command = new SqlCommand
                    (
                    $@"EXECUTE usp_GetOlder {id}"
                    , connection
                    );
                command.ExecuteNonQuery();

                var command2 = new SqlCommand
                    (
                    $@"SELECT Name,Age FROM Minions
                     WHERE Id = {id}"
                    , connection
                    );
                var r = command2.ExecuteReader();

                while (r.Read())
                {
                    Console.WriteLine($"{r["Name"]} - {r["Age"]} years old");
                }
            }
        }
    }
}