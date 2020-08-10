using Domain;
using System.Security.Cryptography.X509Certificates;

namespace Application.Models
{
    public class OrderItemModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool AllowedDelete { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public string Seat { get; set; }


        public OrderItemModel()
        {

        }
        public OrderItemModel(OrderItem orderItem, int quantity)
        {
            Id = orderItem.Id;
            var data = orderItem.Product.Name.Split('-');

            if (orderItem.Product.Category == ProductCategory.Food)
            {
                AllowedDelete = true;
                if (data.Length > 1)
                {
                    Name = data[0] + data[1];
                }
            }
            else if (orderItem.Product.Category == ProductCategory.Ticket)
            {
                if (data.Length > 1)
                {
                    Name = data[0] + data[1];
                    Room = data[2];
                    Seat = data[3];
                }
            }
            Quantity = quantity;
            Amount = orderItem.Amount;
            TotalAmount = orderItem.Amount * quantity;
        }
    }
}

