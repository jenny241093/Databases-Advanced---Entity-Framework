﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class Kitten:Animal
  {


      public Kitten(string name, int age) : base(name, age, "Female")
      {
      }
    public override string ProduceSound()
    {
        return "Meow";
    }
}

