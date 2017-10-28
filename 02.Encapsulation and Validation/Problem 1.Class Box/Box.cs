using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Box
{
    private double length;
    private double width;
    private double height;

    public Box(double length, double width, double height)
    {
        this.Length = length;
        this.Widht = width;
        this.Height = height;
    }
    public double Height
    {
        get { return height; }
        private set { height = value; }
    }


    public double Widht
    {
        get { return width; }
        private set { width = value; }
    }

    public double Length
    {
        get { return length; }
        private set { length = value; }
    }

    public double Surface(double length, double width, double height)
    {
        //Surface Area = 2lw + 2lh + 2wh
        return 2 * Length * Widht + 2 * Length * Height + 2 * Widht * Height;
    }
    public double LateralSurface(double length, double width, double height)
    {
        //Surface Area = 2lw + 2lh + 2wh
        return 2 * Length * Height + 2 * Widht * Height;
    }
    public double Volume(double length, double width, double height)
    {
        //Surface Area = 2lw + 2lh + 2wh
        return Length * Height * Widht;
    }
}
