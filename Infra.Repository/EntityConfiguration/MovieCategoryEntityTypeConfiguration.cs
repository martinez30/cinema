using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    public class MovieCategoryEntityTypeConfiguration : BaseEntityTypeConfiguration<MovieCategory>
    {
        public override void Configure(EntityTypeBuilder<MovieCategory> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Description)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .HasColumnType("varchar(40)")
                   .IsRequired();
        }
    }
}
