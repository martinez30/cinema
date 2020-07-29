using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Session : BaseEntity 
    {
        public Movie Movie { get; set; }
        public Room Room { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool DescontAllowed { get; set; }
        public IList<Ticket> Tickets { get; set; }

        protected Session()
        {
            Tickets = new List<Ticket>();
        }

        public Session(Movie movie, Room room, DateTime date, decimal amount, bool descontAllowed) : this()
        {
            Movie = movie;
            Room = room;
            Date = date;
            Amount = amount;
            DescontAllowed = descontAllowed;
        }
    }
}
