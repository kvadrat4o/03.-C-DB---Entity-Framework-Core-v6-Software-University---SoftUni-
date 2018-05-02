using System.Collections.Generic;

public class Validator
{
    public static bool IsCurrentStatNotInValidRange(double value)
    {
        return value < 0 || value > 100;
    }

    public static bool IsCurrentNameNullEmptyOrWhiteSpase(string value)
    {
        return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
    }

    public static bool PlayerDoseNotExistInThisTeam(string player, List<Player> players)
    {
        return !players.Exists(p => p.Name == player);
    }

    public static bool IsEmptyTeam(int value)
    {
        return value == 0;
    }

    public static bool TeamDoseNotExist(string team, List<Team> teams)
    {
        return !teams.Exists(t => t.Name == team);
    }
}