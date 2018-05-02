using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data.Data.Models.Configurations
{
    class MedicamentConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder
                .Property(m => m.Name)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired();
        }
    }
}
