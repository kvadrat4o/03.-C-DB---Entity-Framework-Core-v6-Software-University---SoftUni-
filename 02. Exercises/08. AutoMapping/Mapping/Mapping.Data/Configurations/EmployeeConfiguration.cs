using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mapping.Models;

namespace Mapping.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.EmployeeId);

            builder.Property(e => e.Address).IsRequired(false).IsUnicode(true);

            builder.Property(e => e.Birthday).IsRequired(false);

            builder.Property(e => e.FirstName).HasMaxLength(25).IsRequired(true).IsUnicode(true);

            builder.Property(e => e.LastName).HasMaxLength(30).IsRequired(true).IsUnicode(true);

            builder.Property(e => e.Salary).IsRequired(true);

            builder.HasMany(e => e.Employees)
                .WithOne(em => em.Manager)
                .HasForeignKey(fk => fk.ManagerId);
        }
    }
}
