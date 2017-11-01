﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public  class FoodFactory
{
    public static Food CreateFood(string foodToString)
    {
        switch (foodToString.ToLower())
        {
            case "cram":
                return new Cram();

            case "lembas":
                return new Lembas();

            case "apple":
                return new Apple();

            case "melon":
                return new Melon();

            case "honeycake":
                return new HoneyCake();

            case "mushrooms":
                return new Mushrooms();

            default:
                return new ErythingElse();
        }
    }
}