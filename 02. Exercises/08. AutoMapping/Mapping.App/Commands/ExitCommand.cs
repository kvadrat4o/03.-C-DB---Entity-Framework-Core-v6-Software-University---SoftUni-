using System;
using System.Collections.Generic;
using System.Text;

namespace Mapping.App.Commands
{
    public class ExitCommand
    {
        public static string Execute()
        {
            Environment.Exit(0);
            return "You are now logged out!";
        }
    }
}
