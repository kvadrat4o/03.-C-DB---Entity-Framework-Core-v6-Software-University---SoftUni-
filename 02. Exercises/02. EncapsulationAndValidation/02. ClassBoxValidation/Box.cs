using System;
public class Box
{
    private double length;

    private double width;

    private double height;


    public double Length
    {
        get
        {
            return this.length;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Length cannot be zero or negative.");
            }
            else
            {
                this.length = value;
            }
        }
    }

    public double Width
    {
        get
        {
            return this.width;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Width cannot be zero or negative.");
            }
            else
            {
                this.width = value;
            }
        
        }
    }

    public double Height
    {
        get
        {
            return this.height;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Height cannot be zero or negative.");
            }
            else
            {
                this.height = value;
            }
        }
    }

    public Box(double length, double width, double height)
    {
        this.Length = length;
        this.Width = width;
        this.Height = height;
    }

    public double GetBoxSurfaceArea()
    {
        return 2 * this.length * this.width + 2 * this.width * this.height + 2 * this.length * this.height;
    }

    public double GetBoxLateralSurfaceArea()
    {
        return 2 * this.length * this.height + 2 * this.width * this.height;
    }
    public double GetBoxVolume()
    {
        return this.width * this.height * this.length;
    }

    public void PrintBox()
    {
        System.Console.WriteLine($"Surface Area - {GetBoxSurfaceArea():F2}\nLateral Surface Area - {GetBoxLateralSurfaceArea():F2}\nVolume - {GetBoxVolume():F2}");
    }
}

