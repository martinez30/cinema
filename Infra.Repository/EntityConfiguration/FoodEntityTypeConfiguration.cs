using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    public class FoodEntityTypeConfiguration : ProductEntityTypeConfiguration<Food>
    {
        public override void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.Property(x => x.Description)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.HasOne(x => x.FoodCategory);
        }
    }
}
