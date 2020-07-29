using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Food : Product
    {
        public string Description { get; set; }
        public FoodCategory FoodCategory { get; set; }

        protected Food()
        {

        }

        public Food(string description, FoodCategory foodCategory, string name, ProductCategory category, decimal amount) : base ( $"{name} - {description}",  category,  amount)
        {
            Description = description;
            FoodCategory = foodCategory;
        }
    }
}
