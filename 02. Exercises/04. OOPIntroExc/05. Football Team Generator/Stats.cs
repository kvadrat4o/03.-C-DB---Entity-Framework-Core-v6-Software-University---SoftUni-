public class Stats
{
    private double endurance;

    private double sprint;

    private double dribble;

    private double passing;

    private double shooting;

    public double Level
    {
        get
        {
            return MathOptions.Avg(this.endurance, this.sprint, this.dribble, this.passing, this.shooting);
        }
    }

    public Stats(double endurance, double sprint, double dribble, double passing, double shooting)
    {
        this.Endurance = endurance;
        this.Sprint = sprint;
        this.Dribble = dribble;
        this.Passing = passing;
        this.Shooting = shooting;
    }

    public double Endurance
    {
        get
        {
            return this.endurance;
        }
        set
        {
            if (Validator.IsCurrentStatNotInValidRange(value))
            {
                Messages.InvalidStatValueException(nameof(this.Endurance));
            }
            this.endurance = value;
        }
    }

    public double Sprint
    {
        get
        {
            return this.sprint;
        }
        set
        {
            if (Validator.IsCurrentStatNotInValidRange(value))
            {
                Messages.InvalidStatValueException(nameof(this.Sprint));
            }
            this.sprint = value;
        }
    }

    public double Dribble
    {
        get
        {
            return this.dribble;
        }
        set
        {
            if (Validator.IsCurrentStatNotInValidRange(value))
            {
                Messages.InvalidStatValueException(nameof(this.Dribble));
            }
            this.dribble = value;
        }
    }

    public double Passing
    {
        get
        {
            return this.passing;
        }
        set
        {
            if (Validator.IsCurrentStatNotInValidRange(value))
            {
                Messages.InvalidStatValueException(nameof(this.Passing));
            }
            this.passing = value;
        }
    }

    public double Shooting
    {
        get
        {
            return this.shooting;
        }
        set
        {
            if (Validator.IsCurrentStatNotInValidRange(value))
            {
                Messages.InvalidStatValueException(nameof(this.Shooting));
            }
            this.shooting = value;
        }
    }
}