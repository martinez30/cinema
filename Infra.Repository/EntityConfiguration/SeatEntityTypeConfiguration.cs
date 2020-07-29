using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    public class SeatEntityTypeConfiguration : BaseEntityTypeConfiguration<Seat>
    {
        public override void Configure(EntityTypeBuilder<Seat> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Room);

            builder.Property(x => x.Row)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .HasColumnType("varchar(2)")
                   .IsRequired();

            builder.Property(x => x.Column)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .IsRequired();
        }
    }
}
