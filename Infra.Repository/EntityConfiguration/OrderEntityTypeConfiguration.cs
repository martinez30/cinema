using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository.EntityConfiguration
{
    public class OrderEntityTypeConfiguration : BaseEntityTypeConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.OrderDate)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .IsRequired();

            builder.HasOne(x => x.Client);
                  
            builder.Property(x => x.PaymentMethodId)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .IsRequired();

            builder.Property(x => x.PaymentOrderId)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .IsRequired();

            builder.Property(x => x.Status)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .IsRequired();

            builder.Property(x => x.TotalAmount)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .IsRequired();

            builder.HasMany(x => x.OrderItems);


        }
    }
}
