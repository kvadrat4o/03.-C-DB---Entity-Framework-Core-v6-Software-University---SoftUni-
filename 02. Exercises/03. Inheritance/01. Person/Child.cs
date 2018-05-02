using System;


class Child: Person
{
    public new int Age
    {
        get
        {
            return base.Age;
        }

        set
        {
            if (base.Age > 15)
            {
                throw new ArgumentException("Child's age must be less than 15!");
            }
            else
            {
                base.Age = value;
            }
        }
    }

    public Child(string name, int age)
    :base(name, age)
    {
        this.Name = name;
        this.Age = age;
    }
}

