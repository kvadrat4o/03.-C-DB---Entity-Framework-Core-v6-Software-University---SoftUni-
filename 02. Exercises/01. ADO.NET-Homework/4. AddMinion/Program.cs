using System;
using System.Data.SqlClient;
using System.Linq;

namespace _4.Add_Minion
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new SqlConnection
                (
                "Server=(localdb)\\MSSQLLocalDB;Database=MinionsDB;Integrated Security=True"
                );

            var minion = Console.ReadLine().Split();
            var name = minion[1];
            var age = int.Parse(minion[2]);
            var town = minion[3];
            var vilian = Console.ReadLine().Split();
            var vName = vilian[1];

            connection.Open();
            using (connection)
            {
                int townId = 0;
                int villainId = 0;
                try
                {
                    townId = FindTownId(connection, town);
                }
                catch (Exception)
                {
                    townId = 0;
                }
                try
                {
                    villainId = FindVillainId(connection, vName);
                }
                catch (Exception)
                {
                    villainId = 0;
                }

                if (villainId != 0 && townId != 0)
                {
                    InsertIntoMinions(connection, name, age, townId, vName);
                    var minionId = FindMinionId(connection, name);

                    InsertIntoMinionsVillains(connection, minionId, villainId);
                }
                else if (villainId == 0 && townId != 0)
                {
                    InsertIntoVillains(connection, vName);
                    InsertIntoMinions(connection, name, age, townId, vName);
                    var minionId = FindMinionId(connection, name);


                    InsertIntoMinionsVillains(connection, minionId, FindVillainId(connection, vName));
                }
                else if (villainId != 0 && townId == 0)
                {
                    InsertIntoTowns(connection, town);
                    var townIdd = FindTownId(connection, town);
                    InsertIntoMinions(connection, name, age, townIdd, vName);
                    var minionId = FindMinionId(connection, name);
                    InsertIntoMinionsVillains(connection, minionId, villainId);
                }
                else
                {
                    InsertIntoTowns(connection, town);
                    InsertIntoVillains(connection, vName);
                    var vilainId = FindVillainId(connection, vName);
                    var townIdd = FindTownId(connection, town);
                    InsertIntoMinions(connection, name, age, townIdd, vName);
                    var minionId = FindMinionId(connection, name);

                    InsertIntoMinionsVillains(connection, minionId, vilainId);
                }
            }
        }

        private static void InsertIntoTowns(SqlConnection connection, string town)
        {
            var insertT = new SqlCommand
                        (
                        $@"INSERT INTO Towns VALUES
                         ('{town}','Uzbekistan')"
                        , connection);
            insertT.ExecuteNonQuery();

            Console.WriteLine($"Town {town} was added to the database.");
        }

        private static int FindTownId(SqlConnection connection, string town)
        {
            var findTown = new SqlCommand(
                    $@"SELECT Id FROM Towns
                       WHERE Name = @name"
                    , connection);
            findTown.Parameters.AddWithValue("@name", town);

            return (int)findTown.ExecuteScalar();
        }

        private static int FindVillainId(SqlConnection connection, string vName)
        {
            var findVillain = new SqlCommand(
                    $@"SELECT Id FROM Villans
                       WHERE Name=@vname"
                    , connection);
            findVillain.Parameters.AddWithValue("@vname", vName);

            return (int)findVillain.ExecuteScalar();
        }

        private static void InsertIntoVillains(SqlConnection connection, string vName)
        {
            var insertV = new SqlCommand
                        (
                        $@"INSERT INTO Villans VALUES
                         ('{vName}','evil')"
                        , connection);
            insertV.ExecuteNonQuery();

            Console.WriteLine($"Villain {vName} was added to the database.");
        }

        private static void InsertIntoMinionsVillains(SqlConnection connection, int minionId, int villainId)
        {
            var insertMV = new SqlCommand
                        (
                        $@"INSERT INTO MinionsVillans VALUES
                         ({minionId},{villainId})"
                        , connection);
            insertMV.ExecuteNonQuery();
        }

        private static void InsertIntoMinions(SqlConnection connection, string name, int age, int townId, string vName)
        {
            var insertM = new SqlCommand
                        (
                        $@"INSERT INTO Minions(Name,Age,TownId) VALUES
                         ('{name}',{age},{townId})"
                        , connection);
            insertM.ExecuteNonQuery();

            Console.WriteLine($"Successfully added {name} to be minion of {vName}.");
        }

        private static int FindMinionId(SqlConnection connection, string name)
        {
            var minionIdfind = new SqlCommand
                       (
                       $@"SELECT Id FROM Minions
                          WHERE Name = '{name}'"
                       , connection);
            return (int)minionIdfind.ExecuteScalar();
        }

    }
}