using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    class PaymentOrderEntityTypeConfiguration : BaseEntityTypeConfiguration<PaymentOrder>
    {
        public override void Configure(EntityTypeBuilder<PaymentOrder> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Card);
                

            builder.Property(x => x.Date)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            builder.Property(x => x.Amount)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            builder.HasMany(x => x.PaymentResponses);
        }
    }
}
