using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    public class FoodCategoryEntityTypeConfiguration : BaseEntityTypeConfiguration<FoodCategory>
    {
        public override void Configure(EntityTypeBuilder<FoodCategory> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired()
                .HasColumnType("varchar(20)");

        }
    }
}
