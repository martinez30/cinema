using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    public abstract class ProductEntityTypeConfiguration<T> : BaseEntityTypeConfiguration<T> where T : Product
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            //Nas Classes derivadas nao se chama base.configure
            base.Configure(builder);

            builder.Property(x => x.Name)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnType("varchar(30)")
                .IsRequired();

            builder.Property(x => x.Category)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            builder.Property(x => x.Amount)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();
        }

    }
}
