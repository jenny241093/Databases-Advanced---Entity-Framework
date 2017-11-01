using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Program
{
    static void Main(string[] args)
    {
        var cars = new List<Car>();
        int n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            var cmdArgs = Console.ReadLine().Split();
            var engine = new Engine(int.Parse(cmdArgs[1]), int.Parse(cmdArgs[2]));
            var cargo = new Cargo(int.Parse(cmdArgs[3]), cmdArgs[4]);
            List<Tire> tires = new List<Tire>();
            var tiresToAdd = cmdArgs.Skip(5).ToList();
            for (int j = 0; j < tiresToAdd.Count; j += 2)
            {
                var tire = new Tire(double.Parse(tiresToAdd[j]), int.Parse(tiresToAdd[j + 1]));
                tires.Add(tire);
            }
            Car car = new Car(cmdArgs[0], engine, cargo, tires);
            cars.Add(car);
        }
        var command = Console.ReadLine();
        if (command=="fragile")
        {
            cars.Where(c => c.Cargo.CargoType == "fragile" && c.Tires.Any(t => t.TirePressure < 1))
                .ToList()
                .ForEach(c => Console.WriteLine(c.Model));
        }
        else
        {
            cars.Where(c => c.Cargo.CargoType == "flammable" && c.Engine.EnginePower>250)
                .ToList()
                .ForEach(c => Console.WriteLine(c.Model));
        }
    }
}
