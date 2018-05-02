using System;

public class DateDifference
{
    public static void Main()
    {
        DateTime firstDate = DateTime.ParseExact(Console.ReadLine(), "yyyy MM dd", null);
        DateTime lastDate = DateTime.ParseExact(Console.ReadLine(), "yyyy MM dd", null);
        Console.WriteLine(DateModifier.DayDifference(firstDate, lastDate));
    }
}