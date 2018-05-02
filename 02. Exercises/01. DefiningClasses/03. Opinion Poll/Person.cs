class Person
{
    public string Name { get; set; }

    public int Age { get; set; }

    public Person()
    {
        this.Name = "No name";
        this.Age = 1;
    }

    public Person(int age)
    {
        this.Age = age;
        this.Name = "No name";
    }

    public Person(int age, string name)
    {
        this.Age = age;
        this.Name = name;
    }
}

