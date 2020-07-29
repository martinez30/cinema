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
        public string DescriptionTicket1 { get; set; }
        public string DescriptionTicket2 { get; set; }
        public string DescriptionTicket3 { get; set; }
        public string DescriptionFood1 { get; set; }


        public OrderItemModel()
        {

        }
        public OrderItemModel(OrderItem orderItem, int quantity)
        {
            Id = orderItem.Id;

            if (orderItem.Product.Category == ProductCategory.Food)
            {
                var data = orderItem.Product.Name.Split('-');
                if (data.Length > 1)
                {
                    Name = data[0];
                    DescriptionFood1 = data[1];
                }
            }
            else if (orderItem.Product.Category == ProductCategory.Ticket)
            {
                var data = orderItem.Product.Name.Split('-');
                if (data.Length > 1)
                {
                    Name = data[0];
                    DescriptionTicket1 = data[1];
                    DescriptionTicket2 = data[2];
                    DescriptionTicket3 = data[3];
                }
            }
            Quantity = quantity;
            Amount = orderItem.Amount;
            TotalAmount = orderItem.Amount * quantity;
        }
    }
}

