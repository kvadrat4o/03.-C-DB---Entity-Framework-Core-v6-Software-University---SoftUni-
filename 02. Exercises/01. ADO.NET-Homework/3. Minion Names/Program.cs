using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace _3._Minion_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            int villan = int.Parse(Console.ReadLine());
            Dictionary<String, int> minions = new Dictionary<string, int>();
            int count = 1;
            var vilan = "";
            var minioName = "";
            var minionAge = 0;
            SqlConnectionStringBuilder build = new SqlConnectionStringBuilder()
            {
                ["Data Source"] = "(localdb)\\MSSQLLocalDB",
                ["Initial Catalog"] = "MinionsDB",
                ["Integrated Security"] = "True"
            };
            try
            {
                SqlConnection connection = new SqlConnection(build.ToString());
                connection.Open();
                using (connection)
                {
                    string sql = $"SELECT v.Name,m.Name,m.Age FROM Minions AS m FULL JOIN MinionsVillans AS mv ON mv.MinionId = m.Id FULL JOIN Villans AS v ON v.Id = mv.VillanId WHERE mv.VillanId = {villan} GROUP BY v.Name,m.Name, m.Age ORDER BY m.Name ASC";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader read = command.ExecuteReader();
                    while (read.Read())
                    {
                        count = 1;
                        vilan = (string)read[0];
                        minioName = (string)read[1];
                        minionAge = (int)read["Age"];
                        minions.Add(minioName, minionAge);
                    }
                    if (villan > 5 || villan <1)
                    {
                        Console.WriteLine($"No villain with ID {villan} exists in the database.");
                    }
                    else
                    {
                        Console.WriteLine($"Villain: {vilan}");
                        foreach (var minion in minions.OrderBy(a => a.Key))
                        {
                            Console.WriteLine($"{count}. {minion.Key} {minion.Value}");
                            count++;
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message); ;
            }
            
        }
    }
}
