using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class FoodCategory : BaseEntity
    {
        public string Name { get; set; }

        protected FoodCategory()
        {

        }

        public FoodCategory(string name)
        {
            Name = name;
        }
    }
}
