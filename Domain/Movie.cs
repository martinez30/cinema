using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Movie : BaseEntity
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public MovieClassification Classification { get; set; }
        public string Synopsis { get; set; }
        public MovieCategory Category { get; set; } 
        public IList<Session> Sessions{ get; set; }

        protected Movie()
        {
            Sessions = new List<Session>();
        }

        public Movie(string name, int duration, MovieClassification classification, string synopsis, MovieCategory category) : this()
        {
            Name = name;
            Duration = duration;
            Classification = classification;
            Synopsis = synopsis;
            Category = category;
        }
    }
}
