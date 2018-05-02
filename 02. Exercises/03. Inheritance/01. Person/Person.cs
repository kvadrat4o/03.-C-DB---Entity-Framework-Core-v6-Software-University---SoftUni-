﻿using System;

class Person
{
    private string name;

    private int age;

    public  string Name
    {
        get
        {
            return this.name;
        }

        set
        {
            if (value.Length < 3)
            {
                throw new ArgumentException("Name's length should not be less than 3 symbols!");
            }
            else
            {
                this.name = value;
            }
        }
    }

    public  int Age
    {
        get
        {
            return this.age;
        }

        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Age must be positive!");
            }
            else
            {
                this.age = value;
            }
        }
    }

    public Person()
    {

    }

    public Person(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }

    public override string ToString()
    {
        return $"Name: {this.Name}, Age: {this.Age}";
    }
}
