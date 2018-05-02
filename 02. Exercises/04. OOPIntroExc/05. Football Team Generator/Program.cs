using System;
using System.Collections.Generic;

public class StartUp
{
    static string[] teamData;

    static List<Team> teams = new List<Team>();

    public static void Main()
    {
        var input = Console.ReadLine();
        while (input != Constants.EndWhileLoop)
        {
            try
            {
                teamData = input.Split(Constants.SplitSymbol);
                if (teamData[Constants.CommandIndex] == Constants.Team)
                {
                    AddNewTeam();
                }
                else if (teamData[Constants.CommandIndex] == Constants.Add)
                {
                    TryAddPlayer();
                }
                else if (teamData[Constants.CommandIndex] == Constants.Remove)
                {
                    TryRemovePlayer();
                }
                else if (teamData[Constants.CommandIndex] == Constants.Rating)
                {
                    PrintTeamRating();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            input = Console.ReadLine();
        }
    }

    public static void AddNewTeam()
    {
        teams.Add(new Team(teamData[Constants.TeamIndex]));
    }

    public static void TryAddPlayer()
    {
        if (Validator.TeamDoseNotExist(teamData[Constants.TeamIndex], teams))
        {
            Messages.ThisTeamDoseNotExist(teamData[Constants.TeamIndex]);
            return;
        }
        AddNewPlayer();
    }

    public static void AddNewPlayer()
    {
        var team = teamData[Constants.TeamIndex];
        var player = teamData[Constants.PlayerIndex];
        var endurance = Convert.ToDouble(teamData[Constants.EnduranceIndex]);
        var sprint = Convert.ToDouble(teamData[Constants.SprintIndex]);
        var dribble = Convert.ToDouble(teamData[Constants.DribbleIndex]);
        var passing = Convert.ToDouble(teamData[Constants.PassingIndex]);
        var shoots = Convert.ToDouble(teamData[Constants.ShootingIndex]);
        var stats = new Stats(endurance, sprint, dribble, passing, shoots);
        var newPlayer = new Player(player, stats);
        teams.Find(t => t.Name == team).Add(newPlayer);
    }

    public static void TryRemovePlayer()
    {
        if (Validator.TeamDoseNotExist(teamData[Constants.TeamIndex], teams))
        {
            Messages.ThisTeamDoseNotExist(teamData[Constants.TeamIndex]);
            return;
        }
        teams.Find(t => t.Name == teamData[Constants.TeamIndex])
            .Remove(teamData[Constants.PlayerIndex]);
    }

    public static void PrintTeamRating()
    {
        if (Validator.TeamDoseNotExist(teamData[Constants.TeamIndex], teams))
        {
            Messages.ThisTeamDoseNotExist(teamData[1]);
            return;
        }
        var rating = Math.Round(teams.Find(t => t.Name == teamData[Constants.TeamIndex]).Rating);
        Console.WriteLine($"{teamData[Constants.TeamIndex]} - {rating}");
    }
}