﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;
using P01_StudentSystem.Data;

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
