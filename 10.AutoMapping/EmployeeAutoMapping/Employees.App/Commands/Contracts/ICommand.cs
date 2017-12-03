using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.App
{
   internal interface ICommand
   {
       string Execute(params string[]args);

   }
}
