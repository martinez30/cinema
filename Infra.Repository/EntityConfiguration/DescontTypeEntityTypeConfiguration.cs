using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    public  class DescontTypeEntityTypeConfiguration : BaseEntityTypeConfiguration<DescontType>
    {
        public override void Configure(EntityTypeBuilder<DescontType> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.Percentage)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();
        }
    }
}
