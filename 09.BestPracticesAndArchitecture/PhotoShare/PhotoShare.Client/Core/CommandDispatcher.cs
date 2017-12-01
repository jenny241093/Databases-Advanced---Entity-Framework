using System.Linq;
using PhotoShare.Client.Core.Commands;

namespace PhotoShare.Client.Core
{
    using System;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            string output = String.Empty;

            var cmdArgs = commandParameters.Skip(1).ToArray();
            switch (commandParameters[0])
            {

                case "RegisterUser":
                    output = RegisterUserCommand.Execute(cmdArgs); break;
                case "AddTown":
                    output = AddTownCommand.Execute(cmdArgs);
                    break;
                case "ModifyUser":
                    output = ModifyUserCommand.Execute(cmdArgs);
                    break;
                case "DeleteUser":
                    output = DeleteUser.Execute(cmdArgs);
                    break;
                   case "AddTag":
                    output = AddTagCommand.Execute(cmdArgs);
                    break;
                case "CreateAlbum":
                    output = CreateAlbumCommand.Execute(cmdArgs);
                    break;
                case "AddTagTo":
                    output = AddTagToCommand.Execute(cmdArgs);
                    break;
                case "AddFriend":
                    output = AddFriendCommand.Execute(cmdArgs);
                    break;
                case "AcceptFriend":
                    output = AcceptFriendCommand.Execute(cmdArgs);
                    break;
                case "ListFriends":
                    output = ListFriendsCommand.Execute(cmdArgs);
                    break;
                case "ShareAlbum":
                    output = ShareAlbumCommand.Execute(cmdArgs);
                    break;
                case "UploadPicture":
                    output = UploadPictureCommand.Execute(cmdArgs);
                    break;
                case "Login":
                    output = LoginCommand.Execute(cmdArgs);
                    break;
                case "Logout":
                    output = LogoutCommand.Execute();
                    break;
                case "Exit":
                    output = ExitCommand.Execute();
                    break;
                default:
                    throw new InvalidOperationException($"Command {commandParameters[0]} not valid!");
            }
            return output;
        }
    }
}
