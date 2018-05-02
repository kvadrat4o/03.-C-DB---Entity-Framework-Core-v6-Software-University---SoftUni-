using Mapping.App.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mapping.App
{
    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            string command = commandParameters.First().ToLower();
            string[] parameters = commandParameters.ToArray();
            int parametersCount = parameters.Length;

            string output = string.Empty;
            switch (command)
            {
                case "addemployee":
                    output = AddEmployeeCommand.Execute(parameters);
                    break;
                case "setbirthday":
                    output = SetBirthdayCommand.Execute(parameters);
                    break;
                case "setaddress":
                    output = SetAddressCommand.Execute(parameters);
                    break;
                case "employeeinfo":
                    output = EmployeeInfoCommand.Execute(parameters);
                    break;
                case "employeepersonalinfo":
                    output = EmployeePersonalInfoCommand.Execute(parameters);
                    break;
                case "setmanager":
                    output = SetManagerCommand.Execute(parameters);
                    break;
                case "managerinfo":
                    output = ManagerInfoCommand.Execute(parameters);
                    break;
                case "listemployeesolderthan":
                    output = ListEmplyeesOlderThan.Execute(parameters);
                    break;
                case "exit":
                    output = ExitCommand.Execute();
                    break;
                default:    throw new InvalidOperationException($"Command {command} not valid!");
                    //break;
            }
            return output;

        }
    }
}
