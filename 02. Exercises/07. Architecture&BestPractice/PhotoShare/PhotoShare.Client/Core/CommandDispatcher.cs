namespace PhotoShare.Client.Core
{
    using PhotoShare.Client.Core.Commands;
    using System;
    using System.Linq;
    using PhotoShare.Models;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            string command = commandParameters.First();
            string[] parameters = commandParameters.ToArray();
            int parametersCount = parameters.Length;

            string output = string.Empty;

            switch (command.ToLower())
            {
                case "registeruser":
                    ValidateCommandParams(command, parameters.Length, 4, true);
                    output = RegisterUserCommand.Execute(parameters);
                    break;
                case "addtown":
                    ValidateCommandParams(command, parameters.Length, 2, true);
                    output = AddTownCommand.Execute(parameters);
                    break;
                case "modifyuser":
                    ValidateCommandParams(command, parameters.Length, 3, true);
                    output = ModifyUserCommand.Execute(parameters);
                    break;
                case "deleteuser":
                    ValidateCommandParams(command, parameters.Length, 1, true);
                    output = DeleteUser.Execute(parameters);
                    break;
                case "addtag":
                    ValidateCommandParams(command, parameters.Length, 1, true);
                    output = AddTagCommand.Execute(parameters);
                    break;
                case "createalbum":
                    ValidateCommandParams(command, parameters.Length, 3, false);
                    output = CreateAlbumCommand.Execute(parameters);
                    break;
                case "addtagto":
                    ValidateCommandParams(command, parameters.Length, 2, true);
                    output = AddTagToCommand.Execute(parameters);
                    break;
                case "makefriends":
                    ValidateCommandParams(command, parameters.Length, 2, true);
                    output = AddFriendCommand.Execute(parameters);
                    break;
                case "acceptfriend":
                    ValidateCommandParams(command, parameters.Length, 2, true);
                    output = AcceptFriendCommand.Execute(parameters);
                    break;
                case "listfriends":
                    ValidateCommandParams(command, parameters.Length, 1, true);
                    output = PrintFriendsListCommand.Execute(parameters);
                    break;
                case "sharealbum":
                    ValidateCommandParams(command, parameters.Length, 3, true);
                    output = ShareAlbumCommand.Execute(parameters);
                    break;
                case "uploadpicture":
                    ValidateCommandParams(command, parameters.Length, 3, true);
                    output = UploadPictureCommand.Execute(parameters);
                    break;
                case "exit":
                    ValidateCommandParams(command, parameters.Length, 0, true);
                    output = ExitCommand.Execute();
                    break;
                default:
                    throw new
                        InvalidOperationException($"Command {command} not valid!");
            }

            return output;
        }

        private void ValidateCommandParams(string cmdName, int paramCount, int neededCount, bool eqBig)
        {
            if ((eqBig && paramCount != neededCount) || (!eqBig && paramCount < neededCount))
            {
                throw new ArgumentException($"Command {cmdName} not valid! Parameters count must be {(eqBig ? "exactly" : "minimum")} {neededCount}!");
            }
        }
    }
}
