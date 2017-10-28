using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


  public  class Employee
  {
      private string name;
      private double salary;
    string position;
      private string department;
      private string email;
      private int age;

      public Employee(string name, double salary, string position, string department, string email, int age)
      {
          this.Name = name;
          this.Salary = salary;
          this.Position = position;
          this.Department = department;
          this.Email = email;
          this.Age = age;
      }
      public Employee(string name, double salary, string position, string department)
      {
          this.Name = name;
          this.Salary = salary;
          this.Position = position;
          this.Department = department;
          this.Email = "n/a";
          this.Age = -1;
      }
      public Employee(string name, double salary, string position, string department,string email)
      {
          this.Name = name;
          this.Salary = salary;
          this.Position = position;
          this.Department = department;
          this.Email =email;
          this.Age = -1;
      }
      public Employee(string name, double salary, string position, string department,int age)
      {
          this.Name = name;
          this.Salary = salary;
          this.Position = position;
          this.Department = department;
          this.Email = "n/a";
          this.Age = age;
      }
    public string Name { get => name; set => name = value; }
    public double Salary { get => salary; set => salary = value; }
    public string Position { get => position; set => position = value; }
    public string Department { get => department; set => department = value; }
    public string Email { get => email; set => email = value; }
    public int Age { get => age; set => age = value; }
      public override string ToString()
      {
          return $"{this.Name} {this.Salary:f2} {this.Email} {this.Age}";
      }
  }

