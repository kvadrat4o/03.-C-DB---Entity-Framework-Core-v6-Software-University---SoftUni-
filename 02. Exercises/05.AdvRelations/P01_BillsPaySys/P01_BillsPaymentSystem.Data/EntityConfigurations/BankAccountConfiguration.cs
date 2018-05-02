using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data
{
    internal class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(ba => ba.BankAccountId);

            builder.Property(ba => ba.SwiftCode).HasMaxLength(20).IsRequired(true).IsUnicode(false);
            builder.Property(ba => ba.Balance).IsRequired(true).IsUnicode(false);
            builder.Property(ba => ba.BankName).HasMaxLength(50).IsRequired(true).IsUnicode(true);
        }
    }
}