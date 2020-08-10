using Domain;
using Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Application.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string IdString => Id.ToString().PadLeft(6, '0');
        public IEnumerable<OrderItemModel> OrderItems { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDateString => OrderDate.ToString("dd/MM/yyy HH:mm");
        public int? ClientId { get; set; }
        public string ClientName { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? PaymentOrderId { get; set; }
        public int? Status { get; set; }
        public string StatusName { get; set; }


        public OrderModel()
        {
        }

        public OrderModel(Order order)
        {
            Id = order.Id;
            if (order.OrderItems != null)
            {
                var grouped = order.OrderItems.GroupBy(x => x.Product);
                if (grouped != null)
                {
                    var orderItems = new List<OrderItemModel>();
                    foreach (var item in grouped)
                    {
                        orderItems.Add(new OrderItemModel(item.First(), item.Count()));
                    }
                    OrderItems = orderItems;
                }
            }
            OrderDate = order.OrderDate;
            ClientId = order.Client?.Id;
            TotalAmount = order.TotalAmount;
            PaymentMethodId = order.PaymentMethodId;
            PaymentOrderId = order.PaymentOrderId;
            Status = (int)order.Status;
            StatusName = order.Status.GetDescription();
            ClientName = order.Client?.FirstName ?? "Compra Interna";
        }
    }
}

