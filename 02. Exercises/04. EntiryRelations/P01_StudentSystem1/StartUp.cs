using P01_StudentSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace P01_StudentSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new StudentSystemContext())
            {
               //db.Database.EnsureDeleted();
               //db.Database.EnsureCreated();
            }
        }
    }
}
