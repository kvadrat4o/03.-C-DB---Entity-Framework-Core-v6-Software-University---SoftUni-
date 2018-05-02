using System;

public class Messages
{
    public static void InvalidStatValueException(string name)
    {
        throw new Exception($"{name} should be between 0 and 100.");
    }

    public static void InvalidNameValueException()
    {
        throw new Exception($"A name should not be empty.");
    }

    public static void PlayerNotExistException(string player, string team)
    {
        throw new Exception($"Player {player} is not in {team} team.");
    }

    public static void ThisTeamDoseNotExist(string name)
    {
        Console.WriteLine($"Team {name} does not exist.");
    }
}