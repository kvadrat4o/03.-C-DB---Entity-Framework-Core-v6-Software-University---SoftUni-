using System;

namespace Homework.Client.Commands
{
    public class Exit
    {
        public static string Execute(string[] command)
        {
            Console.WriteLine("GoodBye !");
            Environment.Exit(0);

            return null;
        }
    }
}