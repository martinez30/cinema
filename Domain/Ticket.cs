using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Ticket : Product
    {
        public Session Session { get; set; }
        public Seat Seat { get; set; }

        protected Ticket()
        {

        }

        public Ticket(Session session, Seat seat) : base ($"{session.Movie.Name} - {session.Date:dd/MM/yyyy HH:mm} - {session.Room.Name} - {seat.Row}{seat.Column}", ProductCategory.Ticket , session.Amount)
        {
            Session = session;
            Seat = seat;
        }
    }
}
