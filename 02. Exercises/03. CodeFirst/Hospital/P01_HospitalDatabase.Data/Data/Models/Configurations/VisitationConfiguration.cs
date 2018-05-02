using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data.Data.Models.Configurations
{
    class VisitationConfiguration : IEntityTypeConfiguration<Visitation>
    {
        public void Configure(EntityTypeBuilder<Visitation> builder)
        {
            builder
                .Property(v => v.Comments)
                .HasMaxLength(250)
                .IsUnicode(true)
                .IsRequired(false);

            builder
                .Property(v => v.Date)
                .HasColumnType("DATETIME2")
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder
                .HasOne(v => v.Patient)
                .WithMany(p => p.Visitations)
                .HasForeignKey(p => p.PatientId);
        }
    }
}
