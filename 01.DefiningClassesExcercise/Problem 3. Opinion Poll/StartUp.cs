using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class StartUp
{
    static void Main(string[] args)
    {
        var persons = new List<Person>();
        var n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            var cmdArgs = Console.ReadLine().Split();
            var name = cmdArgs[0];
            var age = int.Parse(cmdArgs[1]);
            var person=new Person(name,age);
            persons.Add(person);
        }
        foreach (var p in persons.OrderBy(x=>x.Name).Where(x=>x.Age>30))
        {
            Console.WriteLine(p.ToString());
        }

    }
}

