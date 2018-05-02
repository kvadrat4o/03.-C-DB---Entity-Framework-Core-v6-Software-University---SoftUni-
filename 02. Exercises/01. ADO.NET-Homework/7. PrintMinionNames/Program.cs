using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _7.Print_All_Minion_Names
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
                var select = new SqlCommand
                    (
                    @"SELECT Name FROM Minions"
                    , connection);
                var reader = select.ExecuteReader();
                var list = new List<string>();

                while (reader.Read())
                {
                    list.Add((string)reader["Name"]);
                }

                var down = list.Count - 1;
                for (int i = 0; i < list.Count / 2; i++)
                {
                    Console.WriteLine(list[i]);
                    Console.WriteLine(list[down]);
                    down--;
                }
                if (list.Count % 2 != 0)
                    Console.WriteLine(list[down]);


            }
        }
    }
}