using Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models.MovieCategory
{
    public class MovieCategoryModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage =Messages.RequiredField)]
        public string Description { get; set; }

        public MovieCategoryModel()
        {

        }

        public MovieCategoryModel(Domain.MovieCategory moviecategory)
        {
            Id = moviecategory.Id;
            Description = moviecategory.Description;
        }
    }
}
