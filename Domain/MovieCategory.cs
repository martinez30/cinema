using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class MovieCategory : BaseEntity
    {
        public string Description { get; set; }

        protected MovieCategory()
        {

        }

        public MovieCategory(string description)
        {
            Description = description;
        }
    }
}
