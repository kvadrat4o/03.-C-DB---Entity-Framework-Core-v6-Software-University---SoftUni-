using System;
using System.Collections.Generic;
using System.Linq;

public class RowData
{
    static ICollection<Car> cars = new List<Car>();

    static string[] carData;
    static string model;
    static string type;
    static int speed;
    static int power;
    static int weight;

    public static void Main()
    {
        ParseAllCars();
        PrintSearchedCars(Console.ReadLine());
    }

    public static void PrintSearchedCars(string criteria)
    {
        if (criteria.ToLower() == "fragile")
        {
            PrintCarsWithFragile(criteria.ToLower());
        }
        else PrintCarsWithFlammable(criteria.ToLower());
    }

    public static void PrintCarsWithFlammable(string criteria)
    {
        foreach (var car in cars)
        {
            if (car.CargoType == criteria && car.EnginePower > 250)
            {
                Console.WriteLine(car.MakeModel);
            }
        }
    }

    public static void PrintCarsWithFragile(string criteria)
    {
        foreach (var car in cars)
        {
            if (car.CargoType == criteria && car.Tires.Any(t => t.Pressure < 1))
            {
                Console.WriteLine(car.MakeModel);
            }
        }
    }

    public static void ParseAllCars()
    {
        int carsList = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < carsList; i++)
        {
            carData = Console.ReadLine().Split();
            model = carData[0];
            speed = Convert.ToInt32(carData[1]);
            power = Convert.ToInt32(carData[2]);
            weight = Convert.ToInt32(carData[3]);
            type = carData[4];
            Car currentCar = new Car(model, speed, power, weight, type);
            ParseTires(currentCar, carData);
            cars.Add(currentCar);
        }
    }

    public static void ParseTires(Car currentCar, string[] carData)
    {
        for (int index = 5; index < carData.Length; index += 2)
        {
            Tire currentTire = new Tire();
            currentTire.Age = Convert.ToInt32(carData[index + 1]);
            currentTire.Pressure = Convert.ToDouble(carData[index]);
            currentCar.Tires.Add(currentTire);
        }
    }
}