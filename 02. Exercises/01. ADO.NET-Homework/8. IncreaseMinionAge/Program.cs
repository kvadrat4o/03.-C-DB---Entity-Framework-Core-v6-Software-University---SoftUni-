using System;
using System.Data.SqlClient;
using System.Linq;

namespace _8.Increase_Minion_Age
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
                var ids = Console.ReadLine().Split()
                    .Select(int.Parse).ToList();

                var update = new SqlCommand
                    (
                    $@"UPDATE Minions
                    SET Name = LOWER(Name), Age = Age + 1
                    WHERE Id IN ({string.Join(",", ids)})"
                    , connection
                    );
                update.ExecuteNonQuery();

                var select = new SqlCommand
                    (
                    $@"SELECT Name,Age FROM Minions
                      WHERE Id IN ({string.Join(",", ids)})"
                    , connection
                    );

                var reader = select.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["Name"] + " " + reader["Age"]);
                }
            }
        }
    }
}