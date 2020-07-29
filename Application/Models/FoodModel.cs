using Domain;
using Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class FoodModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Messages.RequiredField)]
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductCategory? ProductCategory { get; set; }
        public string ProductCategoryName => ProductCategory?.GetDescription();
        [Required(ErrorMessage = Messages.RequiredField)]
        public decimal Amount { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }


        public FoodModel()
        {
        }

        public FoodModel(Food food)
        {
            Id = food.Id;
            Name = food.Name;
            ProductCategory = food.Category;
            Description = food.Description;
            CategoryId = food.FoodCategory.Id;
            CategoryName = food.FoodCategory.Name;
            Amount = food.Amount;

        }
    }
}

