using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Opinion_Poll
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<Person> persons = new List<Person>();
            for (int i = 0; i < n; i++)
            {
                var info = Console.ReadLine().Split(' ');
                Person currP = new Person();
                currP.Age = int.Parse(info[1]);
                currP.Name = info[0];
                persons.Add(currP);
            }
            foreach (Person pers in persons.Where(pers => pers.Age > 30).OrderBy(a => a.Name))
            {
                Console.WriteLine($"{pers.Name} - {pers.Age}");
            }
        }
    }
}
