using Application.Models;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application

{
    public class OrderApplication
    {
        private readonly ICineContext _context;
        public OrderApplication(ICineContext context)
        {
            _context = context;
        }
        public async Task<int> Create(SessionBuyTicketModel model)
        {
            if (!model.Seats.Any(x => x.Selected))
            {
                throw new Exception("Selecione pelo menos 1 poltrona");
            }
            try
            {

                var session = await _context.Sessions
                    .Include(x => x.Room)
                    .Include(x => x.Movie)
                    .FirstOrDefaultAsync(x => x.Id == model.Id);

                var selectedSeats = model.Seats.Where(x => x.Selected).ToList();
                
                var seats = await _context.Seats.Where(x => selectedSeats.Select(y => y.Id).Contains(x.Id)).ToListAsync();

                var order = new Order(DateTime.Now);

                foreach (var item in seats)
                {
                    var ticket = new Ticket(session, item);
                    var orderitem = new OrderItem(order, ticket, 1, null, 20);
                    order.OrderItems.Add(orderitem);
                }
                order.Calculate();
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return order.Id;
            }
            catch
            {
                throw new Exception("Não foi possível realizar a operação. Tente novamente");
            }
        }
        public async Task<IEnumerable<OrderModel>> ListModelAsync(OrderStatus? status, DateTime? date)
        {
            var query = _context.Orders
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                .Include(x => x.Client)
                .AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(x => x.Status == status);
            }
            if (date.HasValue)
            {
                query = query.Where(x => x.OrderDate.Date == date.Value.Date);
            }

            var orders = await query
                .ToListAsync();
            return orders.Select(x => new OrderModel(x)).OrderBy(x => x.OrderDate.Date);
        }
        public async Task<IEnumerable<Order>> ListAsync(DateTime? date)
        {
            var query = _context.Orders
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                .Include(x => x.Client)
                .AsQueryable();


                query = query.Where(x => x.Status == OrderStatus.Closed);

            if (date.HasValue)
            {
                query = query.Where(x => x.OrderDate.Date == date.Value.Date);
                return await query.ToListAsync();

            }
            query = query.Where(x => x.OrderDate.Date > DateTime.Now);
            return await query.ToListAsync();
        }
        public async Task<OrderModel> GetAsync(int id)
        {
            var order = await _context.Orders
                .Include(x => x.OrderItems)
                    .ThenInclude(x=> x.Product)
                .Include(x => x.Client)
                .FirstOrDefaultAsync(x => x.Id == id);

            return new OrderModel(order);
        }
        public async Task<CheckoutModel> GetCheckoutAsync(int id)
        {
            var checkout = await _context.Orders
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                .Include(x => x.Client)
                .FirstOrDefaultAsync(x => x.Id == id);

            return new CheckoutModel(checkout);
        }
        public async Task<CheckoutModel> Checkout(int id, string productName)
        {
            var order = await _context.Orders
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (order == null)
            {
                throw new Exception("Pedido Não Encontrado");
            }

            var query = _context.Foods.AsQueryable();

            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(x => x.Name.ToLower().Contains(productName.Trim().ToLower()));
            }

            var foods = await query.ToListAsync();

            var model = new CheckoutModel(order);

            model.AddFoods(foods);

            return model;
        }
        public async Task AddOrderItem(int orderId, int foodId)
        {
            var order = await _context.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id == orderId);

            var food = await _context.Foods.FirstOrDefaultAsync(x => x.Id == foodId);

            order.OrderItems.Add(new OrderItem(order, food, 1, null, food.Amount));
            order.Calculate();
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteItem(int id, int orderId)
        {
            var order = await _context.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id == orderId);

            var orderitem = order.OrderItems.FirstOrDefault(x => x.Id == id);
            order.OrderItems.Remove(orderitem);
            order.Calculate();
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
        public async Task<OrderModel> FinalizeOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            order.Status = OrderStatus.Closed;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return new OrderModel(order);
        }
        public async Task Delete(OrderModel model)
        {
            var order = await _context.Orders
                .Include(x => x.OrderItems)
                .Include(x=> x.Client)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
        }

    }
}
