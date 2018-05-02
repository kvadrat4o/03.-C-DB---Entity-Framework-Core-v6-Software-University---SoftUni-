using System;

public class DateModifier
{
    public static int DayDifference(DateTime firstDate, DateTime lastDate)
    {
        return Math.Abs((firstDate - lastDate).Days);
    }
}