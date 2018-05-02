using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data
{
    internal class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(cc => cc.CreditCardId);

            builder.Property(cc => cc.ExpirationDate).IsRequired(true);
            builder.Property(cc => cc.Limit).IsRequired(true);
            builder.Property(cc => cc.MoneyOwed).IsRequired(true);

            builder.Ignore(cc => cc.LimitLeft);
            builder.Ignore(cc => cc.PaymentMethodId);
        }
    }
}