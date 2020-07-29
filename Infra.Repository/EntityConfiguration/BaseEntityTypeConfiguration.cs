using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    public abstract class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                    .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction)
                        .IsRequired();

            builder.Property(x => x.CreatedDate)
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                        .IsRequired();

            builder.Property(x => x.ModifiedDate)
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                        .IsRequired(false);
        }
    }

}
