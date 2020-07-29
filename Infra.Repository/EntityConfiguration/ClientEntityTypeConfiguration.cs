using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Repository.EntityConfiguration
{
    public class ClientEntityTypeConfiguration : BaseEntityTypeConfiguration<Client>
    {
        public override void Configure(EntityTypeBuilder<Client> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FirstName)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .HasColumnType("varchar(50)")
                   .IsRequired();

            builder.Property(x => x.LastName)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .IsRequired()
                   .HasColumnType("varchar(100)");

            builder.Property(x => x.DateOfBirth)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .IsRequired();

            builder.Property(x => x.CPF)
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .IsRequired();

            builder.HasIndex(x => x.CPF).IsUnique();

            builder.HasMany(x => x.Orders);
            builder.HasMany(x => x.Cards);
            
        }

    }

}
