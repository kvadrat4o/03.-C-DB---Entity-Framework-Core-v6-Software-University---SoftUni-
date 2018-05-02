using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data.Data.Models.Configurations
{
    class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(e => e.DoctorId);

            builder.Property(e => e.Name)
                     .IsRequired()
                     .IsUnicode(true)
                     .HasMaxLength(100);

            builder.Property(e => e.Specialty)
                     .IsRequired()
                     .IsUnicode(true)
                     .HasMaxLength(100);

            builder.HasMany(d => d.Visitations)
                     .WithOne(v => v.Doctor)
                     .HasForeignKey(d => d.DoctorId);

        }
    }
}
