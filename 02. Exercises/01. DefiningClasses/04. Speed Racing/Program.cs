using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.Speed_Racing
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Car> racingCars = new List<Car>();

            int countInputLines = Convert.ToInt32(Console.ReadLine());

            for (int count = 0; count < countInputLines; count++)
            {
                string[] carData = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                string model = carData[0];
                double initialFuel = Convert.ToDouble(carData[1]);
                double consumption = Convert.ToDouble(carData[2]);

                if (!racingCars.Exists(x => x.Model == model))
                {
                    Car newCar = new Car(model, initialFuel, consumption);
                    racingCars.Add(newCar);
                }
            }

            string[] commands = Console.ReadLine().Split(' ');

            while (commands[0] != "End")
            {
                if (commands[0] == "Drive")
                {
                    Car currentCar = racingCars.Find(x => x.Model == commands[1]);
                    currentCar.Drive(Convert.ToDouble(commands[2]));
                }

                commands = Console.ReadLine().Split(' ');
            }

            foreach (var car in racingCars)
            {
                Console.WriteLine(car.ToString());
            }
        }
    }
}
