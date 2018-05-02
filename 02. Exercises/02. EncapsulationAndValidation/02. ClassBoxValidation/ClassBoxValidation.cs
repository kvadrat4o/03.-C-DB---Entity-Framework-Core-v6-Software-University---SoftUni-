using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassBox
{
    class ClassBox
    {
        static void Main(string[] args)
        {
            Type boxType = typeof(Box);
            FieldInfo[] fields = boxType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            Console.WriteLine(fields.Count());

            var length = Convert.ToDouble(Console.ReadLine());
            var width = Convert.ToDouble(Console.ReadLine());
            var height = Convert.ToDouble(Console.ReadLine());

            try
            {
                var box = new Box(length, width, height);
                box.PrintBox();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
