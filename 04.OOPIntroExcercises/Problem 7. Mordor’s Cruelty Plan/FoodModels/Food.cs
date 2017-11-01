using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public abstract class Food
   {
       private int pointOfHappines;

       public Food(int pointOfHappines)
       {
           this.pointOfHappines = pointOfHappines;
       }

    public virtual int PointOfHappines { get => pointOfHappines; set => pointOfHappines = value; }
}

