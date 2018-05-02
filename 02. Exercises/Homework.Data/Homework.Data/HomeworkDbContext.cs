using System;
using System.Collections.Generic;
using System.Text;
using Homework.Data.Configuration;
using Homework.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework.Data
{
    public class HomeworkDbContext : DbContext
    {
        public HomeworkDbContext()
        {

        }

        public HomeworkDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(ConfigServer.Config);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EmployeeConfig());
        }
    }
}
