using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using P03_FootballBetting;
using P03_FootballBetting.Data;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.App
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db= new FootballBettingContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
