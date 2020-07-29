using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public Client Client { get; set; }
        public decimal TotalAmount { get; set; }
        public int PaymentMethodId { get; set; }
        public int PaymentOrderId { get; set; }
        public OrderStatus Status { get; set; }
        public IList<OrderItem> OrderItems { get; set; }

        protected Order()
        {
            OrderItems = new List<OrderItem>();
            Status = OrderStatus.Open;
        }
        public Order(DateTime orderDate, Client client, int paymentMethodId, int paymentOrderId) : this(orderDate)
        {
            Client = client;
            PaymentMethodId = paymentMethodId;
            PaymentOrderId = paymentOrderId;
        }

        public Order(DateTime orderDate) : this()
        {
            OrderDate = orderDate;
        }

        public void Calculate()
        {
            TotalAmount = OrderItems.Sum(x => x.TotalAmount);
        }
    }
}
