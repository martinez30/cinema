using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    public class RoomEntityTypeConfiguration : BaseEntityTypeConfiguration<Room>
    {
        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.HasMany(x => x.Seats);

            builder.HasMany(x => x.Sessions);
        }
    }
}
