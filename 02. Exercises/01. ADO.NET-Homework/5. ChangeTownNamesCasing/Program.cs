using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _5.Change_Town_Names_Casing
{
    class Program
    {
        static void Main(string[] args)
        {
            var country = Console.ReadLine();

            var connection = new SqlConnection
                (
                "Server=.;Database=MinionsDB;Integrated Security=True"
                );

            connection.Open();
            using (connection)
            {
                var select2 = new SqlCommand
                   (
                   @"SELECT COUNT(Name) FROM Towns
                    WHERE CountryName = @name AND
                    Name = upper(Name) collate SQL_Latin1_General_CP1_CS_AS",
                   connection
                   );
                select2.Parameters.AddWithValue("@name", country);
                var count = (int)select2.ExecuteScalar();

                var update = new SqlCommand
                    (
                    @"UPDATE Towns
                    SET Name = UPPER(Name)
                    WHERE CountryName = @name",
                    connection
                    );
                update.Parameters.AddWithValue("@name", country);
                update.ExecuteNonQuery();

                var select = new SqlCommand
                    (
                    @"SELECT Name FROM Towns
                    WHERE CountryName = @name",
                    connection
                    );

                select.Parameters.AddWithValue("@name", country);
                var towns = select.ExecuteReader();

                if (count > 0)
                {
                    Console.WriteLine("No town names were affected.");

                }
                else
                {
                    var list = new List<string>();
                    while (towns.Read())
                    {
                        list.Add((string)towns["Name"]);
                    }

                    Console.WriteLine($"{list.Count} town names were affected.");
                    Console.WriteLine("[" + string.Join(", ", list) + "]");
                }
            }
        }
    }
}