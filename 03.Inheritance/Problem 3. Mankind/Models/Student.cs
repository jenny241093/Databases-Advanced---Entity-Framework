using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


    public class Student:Human
    {
        private string facultyNumber;
        public Student(string firstName, string lastName,string facultyNumber) : base(firstName, lastName)
        {
            this.FacultyNumber = facultyNumber;
        }

    public string FacultyNumber
    {
        get { return this.facultyNumber; }
        protected set
        {
            if (value.Length<5||value.Length>10||!IsValidFacultyNumber(value))
            {
                throw new ArgumentException("Invalid faculty number!");
            }
          
            this.facultyNumber = value;
        }
    }
        private bool IsValidFacultyNumber(string value)
        {
            bool isValidFacultyNumber = true;
            foreach (char ch in value)
            {
                if (!char.IsLetterOrDigit(ch))
                {
                    isValidFacultyNumber = false;
                }
            }

            return isValidFacultyNumber;
        }
    public override string ToString()
        {
            var sb=new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($"Faculty number: {this.FacultyNumber}");
        return sb.ToString().Trim();
        }
    }

