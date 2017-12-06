using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.App.Core.Commands;

namespace TeamBuilder.App
{
    public class CommandDispatcher
    {
        public string Dispatch(string input)
        {
            string result=string.Empty;
            string[] inputArgs = input.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
            string commandName = inputArgs.Length > 0 ? inputArgs[0] : string.Empty;
            inputArgs=inputArgs.Skip(1).ToArray();

            switch (commandName)
            {
                case "RegisterUser":
                    RegisterUserCommand registerUser = new RegisterUserCommand();
                    result = registerUser.Execute(inputArgs);
                    break;
                case "Login":
                    LoginCommand logIn = new LoginCommand();
                    result = logIn.Execute(inputArgs);
                    break;
                case "Logout":
                    LogoutCommand logout = new LogoutCommand();
                    result = logout.Execute(inputArgs);
                    break;
                case "DeleteUser":
                    DeleteUserCommand delete = new DeleteUserCommand();
                    result = delete.Execute(inputArgs);
                    break;
                case "CreateEvent":
                    CreateEventCommand createEvent = new CreateEventCommand();
                    result = createEvent.Execute(inputArgs);
                    break;
                case "CreateTeam":
                    CreateTeamCommand createTeam = new CreateTeamCommand();
                    result = createTeam.Execute(inputArgs);
                    break;
                case "InviteToTeam":
                    InviteToTeamCommand invitationCommand = new InviteToTeamCommand();
                    result = invitationCommand.Execute(inputArgs);
                    break;
                case "AcceptInvite":
                    AcceptInviteCommand accept = new AcceptInviteCommand();
                    result = accept.Execute(inputArgs);
                    break;
                case "Exit": ExitCommand exit=new ExitCommand();
                    exit.Execute(inputArgs);                   
                    break;
               
                default:
                    throw new NotSupportedException($"Command {commandName} not supported!");
            }
            return result;
        }
    }
}
