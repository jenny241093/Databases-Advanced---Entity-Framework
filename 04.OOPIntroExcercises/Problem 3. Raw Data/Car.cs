using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


  public   class Car
  {
      private string model;
      private Engine engine;
      private Cargo cargo;
      private List<Tire> tires;

      public Car(string model, Engine engine, Cargo cargo, List<Tire> tires)
      {
          this.model = model;
          this.engine = engine;
          this.cargo = cargo;
          this.tires = tires;
      }

    public string Model { get => model; set => model = value; }
    public Engine Engine { get => engine; set => engine = value; }
    public Cargo Cargo { get => cargo; set => cargo = value; }
    public List<Tire> Tires { get => tires; set => tires = value; }
}

