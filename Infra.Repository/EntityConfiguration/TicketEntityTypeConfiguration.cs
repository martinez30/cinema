using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    public class TicketEntityTypeConfiguration : ProductEntityTypeConfiguration<Ticket>
    {
        public override void Configure(EntityTypeBuilder<Ticket> builder)
        {
           
            builder.HasOne(x => x.Session);

            builder.HasOne(x => x.Seat);
        }
    }
}
