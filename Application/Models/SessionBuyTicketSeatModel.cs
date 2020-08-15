using Domain;
using System.Collections.Generic;
using System.Linq;

namespace Application.Models
{
    public class SessionBuyTicketSeatModel
    {


        public int Id { get; set; }
        public string Row { get; set; }
        public int Column { get; set; }
        public bool Selected { get; set; }
        public bool Avaliable { get; set; }
        public string Description { get; set; }

        public SessionBuyTicketSeatModel()
        {

        }
        
        public SessionBuyTicketSeatModel(Seat seat, IList<Ticket> tickets)
        {
            string seatTicket;
            Id = seat.Id;
            Row = seat.Row;
            Column = seat.Column;
            Description = Row + Column;
            Avaliable = true;
            foreach (var ticket in tickets)
            {
                seatTicket = ticket.Seat.Row + ticket.Seat.Column;
                if (seatTicket == Row + Column)
                {
                    Avaliable = false;
                }
            }

        }

    }
}

