using Domain;
using Infra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Models
{
    public class OrderListModel
    {

        public DateTime? OrderDate { get; set; }
        public OrderStatus? Status { get; set; }
        public string StatusName { get; set; }
        public IEnumerable<OrderModel> Orders { get; set; }
        public bool IsSearch { get; set; }

        public OrderListModel()
        {

        }

        public OrderListModel(IEnumerable<Order> orders)
        {
            Orders = orders.Select(x => new OrderModel(x));
        }
    }
}

