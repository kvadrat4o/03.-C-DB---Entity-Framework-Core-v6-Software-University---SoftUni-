using System.Collections.Generic;
using System.Linq;

public class Car
{
    private string makeModel;

    private int engineSpeed;

    private int enginePower;

    private int cargoWeight;

    private string cargoType;

    public ICollection<Tire> Tires { get; }

    public Car(string model, int speed, int power, int weight, string type)
    {
        this.MakeModel = model;
        this.EngineSpeed = speed;
        this.EnginePower = power;
        this.CargoWeight = weight;
        this.CargoType = type;
        this.Tires = new List<Tire>();
    }

    public string MakeModel
    {
        get
        {
            return this.makeModel;
        }
        set
        {
            this.makeModel = value;
        }
    }

    public int EngineSpeed
    {
        get
        {
            return this.engineSpeed;
        }
        set
        {
            this.engineSpeed = value;
        }
    }

    public int EnginePower
    {
        get
        {
            return this.enginePower;
        }
        set
        {
            this.enginePower = value;
        }
    }

    public int CargoWeight
    {
        get
        {
            return this.cargoWeight;
        }
        set
        {
            this.cargoWeight = value;
        }
    }

    public string CargoType
    {
        get
        {
            return this.cargoType;
        }
        set
        {
            this.cargoType = value;
        }
    }
}