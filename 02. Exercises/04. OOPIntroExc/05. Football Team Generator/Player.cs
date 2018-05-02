public class Player
{
    private string name;

    private Stats stats;

    public Player(string name, Stats stats)
    {
        this.Name = name;
        this.Stats = stats;
    }

    public string Name
    {
        get
        {
            return this.name;
        }
        set
        {
            if (Validator.IsCurrentNameNullEmptyOrWhiteSpase(value))
            {
                Messages.InvalidNameValueException();
            }
            this.name = value;
        }
    }

    public Stats Stats
    {
        get
        {
            return this.stats;
        }
        set
        {
            this.stats = value;
        }
    }
}