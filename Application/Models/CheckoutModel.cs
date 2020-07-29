using Domain;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Application.Models
{
    public class CheckoutModel
    {
        public int Id { get; set; }
        public OrderModel Order { get; set; }
        public IEnumerable<CheckoutFoodModel> Foods { get; set; }

        public CheckoutModel()
        {

        }

        public CheckoutModel(Order order)
        {
            Id = order.Id;
            Order = new OrderModel(order);
        }

        public void AddFoods(IEnumerable<Food> foods)
        {
            Foods = foods.Select(x => new CheckoutFoodModel(x));
        }
    }
}

