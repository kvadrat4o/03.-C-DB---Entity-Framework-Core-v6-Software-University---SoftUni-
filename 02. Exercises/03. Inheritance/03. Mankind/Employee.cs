using System;

public class Employee : Human
{
    private decimal weekSalary;

    private decimal workHours;

    public decimal WeekSalary
    {
        get
        {
            return this.weekSalary;
        }
        set
        {
            if (value < 10)
            {
                throw new Exception("Expected value mismatch! Argument: weekSalary");
            }
            this.weekSalary = value;
        }
    }

    public decimal WorkHours
    {
        get
        {
            return this.workHours;
        }
        set
        {
            if (value < 1 || value > 12)
            {
                throw new Exception("Expected value mismatch! Argument: workHoursPerDay");
            }
            this.workHours = value;
        }
    }

    public Employee(string firstName, string lastName, decimal salary, decimal hours) : base()
    {
        base.FirstName = firstName;
        base.LastName = lastName;
        this.WeekSalary = salary;
        this.WorkHours = hours;
    }

    public decimal SalaryPerHour()
    {
        return this.weekSalary / this.workHours / 5;
    }

    public override string ToString()
    {
        return $"First Name: {base.firstName}\n"
             + $"Last Name: {base.lastName}\n"
             + $"Week Salary: {this.weekSalary:F2}\n"
             + $"Hours per day: {this.workHours:F2}\n"
             + $"Salary per hour: {SalaryPerHour():F2}";
    }
}