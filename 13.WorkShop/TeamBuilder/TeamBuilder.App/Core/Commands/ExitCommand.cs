﻿using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Utilities;

namespace TeamBuilder.App.Core.Commands
{
   public  class ExitCommand
    {
        public string Execute(string[]inputArgs)
        {
            Check.CheckLength(0,inputArgs);
            Environment.Exit(0);
            return "Bye Bye!";
        }
    }
}
