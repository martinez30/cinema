using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public interface ICineContext
    {
        DbSet<Card> Cards { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<DescontType> DescontTypes { get; set; }
        DbSet<FoodCategory> FoodCategories { get; set; }
        DbSet<Food> Foods { get; set; }
        DbSet<MovieCategory> MovieCategories { get; set; }
        DbSet<Movie> Movies { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<PaymentOrder> PaymentOrders { get; set; }
        DbSet<PaymentResponse> PaymentResponses { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<Seat> Seats { get; set; }
        DbSet<Session> Sessions { get; set; }
        DbSet<Ticket> Tickets { get; set; }

        IExecutionStrategy CreateExecutionStrategy();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
