using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public class Worker:Human
   {
       private decimal weekSalary;
       private decimal workHoursPerDay;
        public Worker(string firstName, string lastName,decimal salary, decimal hours) : base(firstName, lastName)
        {
            this.WeekSalary = salary;
            this.WorkHoursPerDay = hours;
        }

    public decimal WeekSalary {
        get { return this.weekSalary; }
        protected set
        {
            if (value<=10)
            {
                throw new ArgumentException("Expected value mismatch! Argument: weekSalary");
            }
            this.weekSalary = value;
        }
    }
    public decimal WorkHoursPerDay {
        get { return this.workHoursPerDay; }
        protected set
        {
            if (value < 1||value >12)
            {
                throw new ArgumentException("Expected value mismatch! Argument: workHoursPerDay");
            }
            this.workHoursPerDay = value;
        }
    }

       private decimal CalculateHourWage()
       {
           var result = this.WeekSalary /( 5 * WorkHoursPerDay);
           return result;
       }

       public override string ToString()
       {
        var sb=new StringBuilder();
           sb.AppendLine(base.ToString());
           sb.AppendLine($"Week Salary: {this.WeekSalary:f2}");
           sb.AppendLine($"Hours per day: {this.WorkHoursPerDay:f2}");
           sb.AppendLine($"Salary per hour: {this.CalculateHourWage():f2}");
           return sb.ToString().Trim();
       }
   }
