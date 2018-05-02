using System;

namespace P01_HospitalDatabase 
{
    using P01_HospitalDatabase.Data;

    public class Program
    {
        public static void Main(string[] args)
        {
            var dbContext = new HospitalContext();
        }
    }
}
