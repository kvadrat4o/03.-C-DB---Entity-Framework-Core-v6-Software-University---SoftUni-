using System;
using System.Linq;

public abstract class Human
{
    protected string firstName;

    protected string lastName;

    public virtual string FirstName
    {
        get
        {
            return this.firstName;
        }
        set
        {
            if (value[0] < 'A' || value[0] > 'Z')
            {
                throw new Exception("Expected upper case letter! Argument: firstName");
            }
            if (value.Length < 4)
            {
                throw new Exception("Expected length at least 4 symbols! Argument: firstName");
            }
            this.firstName = value;
        }
    }

    public virtual string LastName
    {
        get
        {
            return this.lastName;
        }
        set
        {
            if (value[0] < 'A' || value[0] > 'Z')
            {
                throw new Exception("Expected upper case letter! Argument: lastName");
            }
            if (value.Length < 3)
            {
                throw new Exception("Expected length at least 3 symbols! Argument: lastName");
            }
            this.lastName = value;
        }
    }

    public Human()
    {

    }
}