using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    public class SessionEntityTypeConfiguration : BaseEntityTypeConfiguration<Session>
    {
        public override void Configure(EntityTypeBuilder<Session> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Movie);

            builder.HasOne(x => x.Room);
            
            builder.Property(x => x.Date)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            builder.Property(x => x.Amount)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            builder.Property(x => x.DescontAllowed)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            builder.HasMany(x => x.Tickets);
        }
    }
}
