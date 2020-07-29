using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    class OrderItemEntityTypeConfiguration : BaseEntityTypeConfiguration<OrderItem>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Order);

            builder.HasOne(x => x.Product);

            builder.Property(x => x.Quantity)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .IsRequired();

            builder.HasOne(x => x.DescontType);
                 
            builder.Property(x => x.Amount)
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .IsRequired();

            builder.Property(x => x.TotalAmount)
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .IsRequired();

        }
    }
}
