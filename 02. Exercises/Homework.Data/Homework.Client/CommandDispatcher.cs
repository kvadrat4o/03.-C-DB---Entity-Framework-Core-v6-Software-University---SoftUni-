using System;
using System.Collections.Generic;
using System.Text;
using Homework.Client.Commands;
using Remotion.Linq.Parsing;

namespace Homework.Client
{
    public class CommandDispatcher
    {
        public string DispatchCommand(string[] command)
        {
            if (command[0].ToLower() == "addemployee" && command.Length == 4)
            {
                return AddEmployee.Execute(command);
            }

            if (command[0].ToLower() == "setbirthday" && command.Length == 4)
            {
                return SetBirthday.Execute(command);
            }

            if (command[0].ToLower() == "setaddress" && command.Length >= 3 && char.IsNumber(command[1], 0))
            {
                return SetAddress.Execute(command);
            }

            if (command[0].ToLower() == "employeeinfo" && command.Length == 2)
            {
                return EmployeeInfo.Execute(command);
            }

            if (command[0].ToLower() == "employeepersonalinfo" && command.Length == 2)
            {
                return EmployeePersonalInfo.Execute(command);
            }

            if (command[0].ToLower() == "exit" && command.Length == 1)
            {
                return Exit.Execute(command);
            }

            if (command[0].ToLower() == "setmanager" && command.Length == 3)
            {
                return SetManager.Execute(command);
            }

            if (command[0].ToLower() == "managerinfo" && command.Length == 2)
            {
                return ManagerInfo.Execute(command);
            }

            if (command[0].ToLower() == "listemployeesolderthan" && command.Length == 2)
            {
                return ListEmployeesOlderThan.Execute(command);
            }

            return "Invalid Command";
        }
    }
}
