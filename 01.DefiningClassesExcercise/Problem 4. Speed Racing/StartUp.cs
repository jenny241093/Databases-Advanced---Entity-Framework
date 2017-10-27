using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public class StartUp
    {

    static void Main(string[] args)
        {
        var cars=new List<Car>();
            var n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var cmdArgs = Console.ReadLine().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                var model = cmdArgs[0];
                var fuelAmount = double.Parse(cmdArgs[1]);
                var fuelCons = double.Parse(cmdArgs[2]);
            var car=new Car(model,fuelAmount,fuelCons);
            cars.Add(car);
            }
            var command = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        while (command[0]!="End")
        {
            var model = command[1];
            var kmToDrive = double.Parse(command[2]);
            var carToBeDriven = cars.FirstOrDefault(x => x.Model == model);
            carToBeDriven.Drive(kmToDrive);

            command= Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }
            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
        }
    }

