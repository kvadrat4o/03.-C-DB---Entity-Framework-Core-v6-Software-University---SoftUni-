using System;
using System.Data.SqlClient;

namespace _01._Initial_Setup
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder build = new SqlConnectionStringBuilder()
            {
                ["Data source"] = "(localdb)\\MSSQLLocalDB",
                ["Integrated Security"] = true

            };
            var connection = new SqlConnection(build.ToString());

            connection.Open();
            using (connection)
            {
                try
                {
                    var comand = new SqlCommand("create database MinionsDB", connection);
                    comand.ExecuteNonQuery();

                }
                catch (Exception exc)
                {

                    Console.WriteLine(exc.Message);
                }

            }

            build["Initial catalog"] = "MinionsDB";
            connection = new SqlConnection(build.ToString());
            connection.Open();
            using (connection)
            {
                try
                {
                    string countriesSQL = "CREATE TABLE Countries (Id int PRIMARY KEY IDENTITY,Name varchar(max))";
                    string townsSQL = "CREATE TABLE Towns (Id int PRIMARY KEY IDENTITY,Name varchar(max),CountryId int,CONSTRAINT Fk_CountriesTowns_ID FOREIGN KEY(CountryId) REFERENCES Countries(Id))";
                    string minionsSQL = "CREATE TABLE Minions(Id int PRIMARY KEY IDENTITY,Name varchar(max) NOT NULL,Age int,TownId int,CONSTRAINT FK_Towns FOREIGN KEY(TownId) REFERENCES Towns(Id))";
                    string evilnessSQL = "CREATE TABLE EvilnessFactors (Id int PRIMARY KEY IDENTITY,Name varchar(max))";
                    string villansSQL = "CREATE TABLE Villans (Id int IDENTITY PRIMARY KEY,Name varchar(max),EvilnessFactorId int,CONSTRAINT Fk_Villans_ID FOREIGN KEY(EvilnessFactorId) REFERENCES EvilnessFactors(Id))";
                    string minionsVillans = "CREATE TABLE MinionsVillans (MinionId int,VillanId int CONSTRAINT Pk_MinionsVillans_ID PRIMARY KEY(MinionId, VillanId),CONSTRAINT Fk_MinionsVillans_V FOREIGN KEY(VillanId) REFERENCES Villans(Id),CONSTRAINT Fk_MinionsVillans_M FOREIGN KEY(MinionId) REFERENCES Minions(Id))";

                    ExecCommand(countriesSQL, connection);
                    ExecCommand(townsSQL, connection);
                    ExecCommand(minionsSQL, connection);
                    ExecCommand(evilnessSQL, connection);
                    ExecCommand(villansSQL, connection);
                    ExecCommand(minionsVillans, connection);

                    string insertCountriesSQL = "INSERT INTO Countries VALUES('Bulgaria'),('United Kingdom'),('Spain'),('France'),('Germany')";
                    string insertTownsSQL = "INSERT INTO Towns(Name, CountryId) VALUES('Sofia',1),('Paris',4),('Berlin',5),('London',2),('Barselona',3)";
                    string insertMinionsSQL = "INSERT INTO Minions(Name, Age, TownId) VALUES('Bob',12,3),('Kevin',23,5),('Steward',9,1),('Mel',3,2),('Dave',7,4)";
                    string insertEvilnessSQL = "INSERT INTO EvilnessFactors(Name) VALUES('super good'),('good'),('bad'),('evil'),('super evil')";
                    string insertVillansSQL = "INSERT INTO Villans(Name,EvilnessFactorId ) VALUES('Gru',5),('Scarlet OverKill',4),('Baltazar Bratt',2),('Vector',1),('Eduardo Perex',3)";
                    string insertMinionsVillans = "INSERT INTO MinionsVillans VALUES(1, 2), (3, 1), (1, 3), (3, 3), (4, 1), (2, 2), (1, 1), (3, 4), (1, 4), (1, 5), (5, 1)";

                    ExecCommand(insertCountriesSQL, connection);
                    ExecCommand(insertTownsSQL, connection);
                    ExecCommand(insertMinionsSQL, connection);
                    ExecCommand(insertEvilnessSQL, connection);
                    ExecCommand(insertVillansSQL, connection);
                    ExecCommand(insertMinionsVillans, connection);
                }
                catch (Exception excep)
                {

                    Console.WriteLine(excep.Message); ;
                }

            }
        }

        private static void ExecCommand(string comand, SqlConnection connection)
        {
            SqlCommand cmd = new SqlCommand(comand,connection);
            cmd.ExecuteNonQuery();
        }
    }
}
