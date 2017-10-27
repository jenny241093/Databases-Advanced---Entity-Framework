using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Car
{
    private string model;
    private double fuelAmount;
    private double fuelConsumptionPerKm;
    private double distanceTravelled;

    public Car(string model, double fuelAmount, double fuelConsumptionPerKm)
    {
        this.Model = model;
        this.FuelAmount = fuelAmount;
        this.FuelConsumptionPerKm = fuelConsumptionPerKm;
        this.DistanceTravelled = 0;
    }
    public string Model { get; private set; }
    public double FuelAmount { get; private set; }
    public double FuelConsumptionPerKm { get; private set; }
    public double DistanceTravelled { get; private set; }
    public void Drive(double kmToDrive)
    {
        var cons = kmToDrive * this.FuelConsumptionPerKm;
        if (cons > this.FuelAmount)
        {
            Console.WriteLine("Insufficient fuel for the drive");
        }
        else
        {
            this.DistanceTravelled += kmToDrive;
            this.FuelAmount -= cons;
           
        }
   
    }

    public override string ToString()
    {
        return $"{this.Model} {this.FuelAmount:F2} {this.DistanceTravelled}";
    }
}
