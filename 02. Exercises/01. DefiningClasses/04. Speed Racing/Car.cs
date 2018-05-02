
class Car
{
    private string model;

    private double fuelAmount;

    private double fuelConsumption;

    private double distanceTraveled;

    public string Model
    {
        get { return this.model; }
        set { this.model = value; }
    }

    public double FuelAmount
    {
        get { return this.fuelAmount; }
        set { this.fuelAmount = value; }
    }

    public double FuelConsumption
    {
        get { return this.fuelConsumption; }
        set { this.fuelConsumption = value; }
    }

    public double DistanceTraveled
    {
        get { return this.distanceTraveled; }
        set { this.distanceTraveled = value; }
    }

    private Car()
    {
        this.DistanceTraveled = 0;
    }

    public Car(string model, double fuelAmount, double fuelConsumption) : this()
    {
        this.Model = model;
        this.FuelAmount = fuelAmount;
        this.FuelConsumption = fuelConsumption;
    }

    public void Drive(double traveledDistance)
    {
        double currentComnsumption = traveledDistance * this.FuelConsumption;

        if (this.FuelAmount - currentComnsumption >= 0)
        {
            this.FuelAmount -= currentComnsumption;
            this.DistanceTraveled += traveledDistance;
        }
        else
        {
            System.Console.WriteLine("Insufficient fuel for the drive");
        }
    }
    public override string ToString()
    {
        return $"{this.Model} {this.FuelAmount:F2} {this.DistanceTraveled}";
    }
}
