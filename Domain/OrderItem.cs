using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OrderItem : BaseEntity 
    {
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DescontType DescontType { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }

        protected OrderItem()
        {

        }

        public OrderItem(Order order, Product product, int quantity, DescontType descontType, decimal amount)
        {
            Order = order;
            Product = product;
            Quantity = quantity;
            DescontType = descontType;
            Amount = amount;
            TotalAmount = amount * quantity;
        }
    }
}
