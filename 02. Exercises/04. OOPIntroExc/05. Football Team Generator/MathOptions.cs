using System.Collections.Generic;
using System.Linq;

public class MathOptions
{
    public static double Avg(double value1, double value2, double value3, double value4, double value5)
    {
        return (value1 + value2 + value3 + value4 + value5) / Constants.StatsCount;
    }

    public static double Rating(List<Player> players)
    {
        if (Validator.IsEmptyTeam(players.Count))
        {
            return 0;
        }
        return players.Average(p => p.Stats.Level);
    }
}