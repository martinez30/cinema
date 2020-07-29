using Application;
using Domain;
using Infra.Repository.EntityConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class CineContext : IdentityDbContext, ICineContext
    {
        public CineContext(DbContextOptions<CineContext>options) : base (options)
        {
            
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<DescontType> DescontTypes { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<MovieCategory> MovieCategories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PaymentOrder> PaymentOrders { get; set; }
        public DbSet<PaymentResponse> PaymentResponses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public IExecutionStrategy CreateExecutionStrategy()
        {
            return Database.CreateExecutionStrategy();
        }
        /// <summary>
        /// Task(tarefa em segundo plano)
        /// <int> (retorno da tarefa)
        /// cancellation(opcional, força a interrupção da execução async)
        /// </summary>

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.SetModifiedDate(DateTime.Now);
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfigurationsFromAssembly(typeof(CineContext).Assembly);

            base.OnModelCreating(modelbuilder);
        }
    }
}
