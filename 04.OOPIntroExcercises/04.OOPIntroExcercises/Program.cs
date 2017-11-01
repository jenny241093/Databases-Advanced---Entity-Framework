﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Problem_3.Oldest_Family_Member
{
    class Program
    {
        static void Main(string[] args)
        {
            var numberOfPeople = int.Parse(Console.ReadLine());
            var family=new Family();
            for (int i = 0; i < numberOfPeople; i++)
            {
                var personInfo = Console.ReadLine().Split();
                var personName = personInfo[0];
                var personAge = int.Parse(personInfo[1]);
                var person=new Person(personName,personAge);
                family.AddMember(person);
            }
            var oldestPerson = family.GetOldestMember();
            Console.WriteLine(family.ToString());
        }
    }
}
