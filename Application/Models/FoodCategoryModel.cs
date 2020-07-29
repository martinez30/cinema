using Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models
{
    public class FoodCategoryModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage =Messages.RequiredField)]
        public string Name{ get; set; }

        public FoodCategoryModel()
        {

        }

        public FoodCategoryModel(Domain.FoodCategory foodcategory)
        {
            Id = foodcategory.Id;
            Name = foodcategory.Name;
        }
    }
}
