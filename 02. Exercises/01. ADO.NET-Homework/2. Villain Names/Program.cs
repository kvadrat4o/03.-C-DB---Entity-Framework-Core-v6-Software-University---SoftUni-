using System;
using System.Linq;
using System.Data.SqlClient;

namespace _2._Villain_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder build = new SqlConnectionStringBuilder()
            {
                ["Data Source"] = "(localdb)\\MSSQLLocalDB",
                ["Integrated Security"] = true,
                ["Initial Catalog"] = "MinionsDB"
            };

            var connection = new SqlConnection(build.ToString());
            connection.Open();
            using (connection)
            {
                try
                {
                    string sqlQuery = "SELECT v.Name, COUNT(mv.MinionId) AS [Minions] FROM Villans AS v FULL JOIN MinionsVillans AS mv ON  mv.VillanId = v.Id GROUP BY v.Name HAVING COUNT(mv.MinionId) > 3 ORDER BY Minions DESC";
                    SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        var vilan = (string)read["Name"];
                        var minions = (int)read["Minions"];
                        Console.WriteLine(String.Format($"('{vilan}') - ('{minions}')", read[0]));
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
