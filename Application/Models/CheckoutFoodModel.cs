using Domain;

namespace Application.Models
{
    public class CheckoutFoodModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public CheckoutFoodModel()
        {

        }
        public CheckoutFoodModel(Food food)
        {
            Id = food.Id;
            Name = food.Name;
            Amount = food.Amount;
            Description = food.Description;
        }

    }
}

