using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public abstract class Mood
    {
        public override string ToString()
        {
            return $"{this.GetType().Name}";
        }
    }
