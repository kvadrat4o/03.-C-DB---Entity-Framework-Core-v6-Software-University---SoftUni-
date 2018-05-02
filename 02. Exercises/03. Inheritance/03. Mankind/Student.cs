using System;
using System.Linq;

public class Student : Human
{
    private string studentID;

    public string StudentID
    {
        get
        {
            return this.studentID;
        }
        set
        {
            if ((value.Length < 5 || value.Length > 10) || value.ToCharArray()
                .Any(s => (s < 'a' || s > 'z') && (s < 'A' || s > 'Z') && (s < '0' || s > '9')))
            {
                throw new Exception("Invalid faculty number!");
            }
            this.studentID = value;
        }
    }

    public Student(string firsName, string lastName, string studentID) : base()
    {
        base.FirstName = firsName;
        base.LastName = lastName;
        this.StudentID = studentID;
    }

    public override string ToString()
    {
        return $"First Name: {base.firstName}\n"
             + $"Last Name: {base.lastName}\n"
             + $"Faculty number: {this.studentID}";
    }
}