﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.App
{
   public class Engine
   {
       private CommandDispatcher commandDispatcher;

       public Engine( CommandDispatcher commandDispatcher)
       {
           this.commandDispatcher = commandDispatcher;

       }
        public void Run()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    string output = this.commandDispatcher.Dispatch(input);
                    Console.WriteLine(output);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.GetBaseException().Message);
                   
                }
            }
        }
    }
}
