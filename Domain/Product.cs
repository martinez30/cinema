using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public abstract class Product : BaseEntity
    { 

        public string Name { get; set; }
        public ProductCategory Category { get; set; }
        public decimal Amount { get; set; }

        protected Product()
        {

        }

        public Product(string name, ProductCategory category, decimal amount)
        {
            Name = name;
            Category = category;
            Amount = amount;
        }
    }
}
