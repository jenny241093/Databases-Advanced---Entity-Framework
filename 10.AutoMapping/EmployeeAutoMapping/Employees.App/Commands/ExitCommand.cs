using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App.Commands
{
    class ExitCommand:ICommand
    {
        public string Execute(params string[] args)
        {
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
            return String.Empty ;
        }
    }
}
