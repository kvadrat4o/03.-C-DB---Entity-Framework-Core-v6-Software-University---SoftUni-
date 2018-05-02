using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data.Data.Models.Configurations

{
    public class DiagnoseConfiguration : IEntityTypeConfiguration<Diagnose>
    {
        public void Configure (EntityTypeBuilder<Diagnose> builder)
        {
            builder
               .Property(d => d.Name)
               .IsUnicode(true)
               .HasMaxLength(50)
               .IsRequired();

            builder
                .Property(d => d.Comments)
                .HasMaxLength(250)
                .IsUnicode(true)
                .IsRequired(false);

            builder
                .HasKey(d => d.DiagnoseId);

            builder
                .HasOne(d => d.Patient)
                .WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.PatientId);
        }
    }
}
