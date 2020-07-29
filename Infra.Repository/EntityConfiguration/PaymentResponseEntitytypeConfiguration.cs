using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    public class PaymentResponseEntitytypeConfiguration : BaseEntityTypeConfiguration<PaymentResponse>
    {
        public override void Configure(EntityTypeBuilder<PaymentResponse> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.PaymentOrder);

            builder.Property(x => x.Status)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .IsRequired();

            builder.Property(x => x.Date)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .IsRequired();

            builder.Property(x => x.NSU)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .HasColumnType("varchar(20)")
                   .IsRequired();

        }
    }
}
