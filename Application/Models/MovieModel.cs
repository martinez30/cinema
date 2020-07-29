using Domain;
using Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
               
        [Required(ErrorMessage = Messages.RequiredField)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = Messages.RequiredField)]
        public int? Duration { get; set; }
        
        [Required(ErrorMessage = Messages.RequiredField)]
        public string Synopsis { get; set; }

        [Required(ErrorMessage = Messages.RequiredField)]
        public MovieClassification? Classification { get; set; }
        public string ClassificationName => Classification?.GetDescription();

        [Required(ErrorMessage = Messages.RequiredField)]
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }


        public MovieModel()
        {

        }

        public MovieModel(Domain.Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            Duration = movie.Duration;
            Synopsis = movie.Synopsis;
            Classification = movie.Classification;
            CategoryId = movie.Category.Id;
            CategoryName = movie.Category.Description;
        }
    }
}

