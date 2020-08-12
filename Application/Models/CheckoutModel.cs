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
        public string ProductName { get; set; }


        public CheckoutModel()
        {

        }

        public CheckoutModel(Order order)
        {
            Id = order.Id;
            Order = new OrderModel(order);
            foreach (var item in order.OrderItems)
            {
                if(item.Product.Category == ProductCategory.Ticket)
                {
                    var data = item.Product.Name.Split('-');
                    ProductName = data[0] + data[1];
                    break;
                }
            }
        }

        public void AddFoods(IEnumerable<Food> foods)
        {
            Foods = foods.Select(x => new CheckoutFoodModel(x));
        }
    }
}

