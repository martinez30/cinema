using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    public class MovieEntityTypeConfiguration : BaseEntityTypeConfiguration<Movie>
    {
        public override void Configure(EntityTypeBuilder<Movie> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(x => x.Duration)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            builder.Property(x => x.Classification)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            builder.Property(x => x.Synopsis)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnType("varchar(512)")
                .IsRequired();

            builder.HasOne(x => x.Category);

            builder.HasMany(x => x.Sessions);


        }
    }
}
