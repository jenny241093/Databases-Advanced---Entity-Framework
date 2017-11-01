﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


    public class Program
    {
        static void Main(string[] args)
        {
            var foods = new List<Food>();

            var foodTokens = Regex.Split(Console.ReadLine(), @"\s+");

            foreach (var foodToken in foodTokens)
            {
                var currentFood = FoodFactory.CreateFood(foodToken);
                foods.Add(currentFood);
            }

            Mood mood = MoodFactory.CreateMood(foods);

            Console.WriteLine(foods.Sum(f => f.PointOfHappines));
            Console.WriteLine(mood);
        }
    }

