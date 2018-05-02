using System;

public class Mankind
{
    static string[] input = new string[] { };

    public static void Main()
    {
        try
        {
            input = Console.ReadLine().Split(' ');
            var student = new Student(input[0], input[1], input[2]);

            input = Console.ReadLine().Split(' ');
            var salary = Convert.ToDecimal(input[2]);
            var hours = Convert.ToDecimal(input[3]);
            var employee = new Employee(input[0], input[1], salary, hours);

            Console.WriteLine(student.ToString());
            Console.WriteLine();
            Console.WriteLine(employee.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}