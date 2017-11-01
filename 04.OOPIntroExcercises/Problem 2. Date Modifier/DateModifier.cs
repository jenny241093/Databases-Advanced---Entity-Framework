using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public  class DateModifier
   {
       private string firstDate;
       private string secondDate;
       public string FirstDate { get; private set; }
       public string SecondDate { get; private set; }

       public DateModifier(string firstDate, string secondDate)
       {
           this.FirstDate = firstDate;
           this.SecondDate = secondDate;
       }
       public double CalculateDifferenceBetweenDates()
       {
           DateTime firstDate = Convert.ToDateTime(this.FirstDate);
           DateTime sedondDate = Convert.ToDateTime(this.SecondDate);
           TimeSpan diff = firstDate - sedondDate;
           return diff.TotalDays;
       }
   }

