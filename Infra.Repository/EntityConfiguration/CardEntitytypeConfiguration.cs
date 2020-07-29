using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    public class CardEntitytypeConfiguration : BaseEntityTypeConfiguration<Card>
    {
        public override void Configure(EntityTypeBuilder<Card> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Token)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(x => x.Brand)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnType("varchar(30)")
                .IsRequired();

            builder.Property(x => x.LastFour)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnType("varchar(4)")
                .IsRequired();

            builder.HasOne(x => x.Client);
            builder.HasMany(x => x.PaymentOrders);
        }
    }
}
