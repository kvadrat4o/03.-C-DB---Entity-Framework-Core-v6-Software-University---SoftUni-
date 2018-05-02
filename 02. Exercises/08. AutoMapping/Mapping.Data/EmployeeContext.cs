using Mapping.Data.Configurations;
using Mapping.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mapping.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext()
        {

        }

        public EmployeeContext(DbContextOptions options) :base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.connenctionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EmployeeConfiguration());
        }
    }

}
