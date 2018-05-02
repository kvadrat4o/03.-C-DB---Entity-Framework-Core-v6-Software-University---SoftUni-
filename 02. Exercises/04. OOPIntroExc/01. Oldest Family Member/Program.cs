using System;
using System.Reflection;

public class FamilyMembers
{
    public static void Main()
    {
        MethodInfo oldestMemberMethod = typeof(Family).GetMethod("GetOldestMember");
        MethodInfo addMemberMethod = typeof(Family).GetMethod("AddMember");
        if (oldestMemberMethod == null || addMemberMethod == null)
        {
            throw new Exception();
        }

        Family family = new Family();
        int iterations = Convert.ToInt32(Console.ReadLine());

        for (int count = 0; count < iterations; count++)
        {
            string[] input = Console.ReadLine().Split();
            string personName = input[0];
            int personAge = Convert.ToInt32(input[1]);
            Person currentMember = new Person(personName, personAge);
            family.AddMember(currentMember);
        }

        Console.WriteLine(family.GetOldestMember().ToString());
    }