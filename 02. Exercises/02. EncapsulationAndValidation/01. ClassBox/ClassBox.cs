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

            var box = new Box();
            box.Length= double.Parse(Console.ReadLine());
            box.Width= double.Parse(Console.ReadLine());
            box.Height= double.Parse(Console.ReadLine());
            box.PrintBox();
        }
    }
}
