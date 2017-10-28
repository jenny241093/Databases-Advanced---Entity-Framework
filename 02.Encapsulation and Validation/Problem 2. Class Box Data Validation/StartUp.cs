using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Problem_2.Class_Box_Data_Validation
{
   public class StartUp
    {
        static void Main(string[] args)
        {
            Type boxType = typeof(Box);
            FieldInfo[] fields = boxType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            Console.WriteLine(fields.Count());
            var length = double.Parse(Console.ReadLine());
            var width = double.Parse(Console.ReadLine());
            var height = double.Parse(Console.ReadLine());
            try
            {

                var box = new Box(length, width, height);

                Console.WriteLine($"Surface Area - {box.Surface(length, width, height):F2}");
                Console.WriteLine(($"Lateral Surface Area - {box.LateralSurface(length, width, height):F2}"));
                Console.WriteLine(($"Volume - {box.Volume(length, width, height):F2}"));
            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }
        }
    }
}
