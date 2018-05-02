using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data.Data.Models.Configurations
{
    class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure (EntityTypeBuilder<Patient> builder)
        {
            builder
                .Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired();

            builder
                .Property(p => p.LastName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired();

            builder
                .Property(p => p.Address)
                .HasMaxLength(250)
                .IsUnicode(true)
                .IsRequired();

            builder
                .Property(p => p.Email)
                .HasMaxLength(80)
                .IsUnicode(false)
                .IsRequired();

            builder
                .Property(p => p.HasInsurance)
                .HasDefaultValue(true);

            builder
                .HasMany(p => p.Diagnoses)
                .WithOne(d => d.Patient)
                .HasForeignKey(d => d.PatientId);

            builder
            .HasMany(p => p.Visitations)
            .WithOne(v => v.Patient)
            .HasForeignKey(v => v.PatientId);
        }
    }
}
