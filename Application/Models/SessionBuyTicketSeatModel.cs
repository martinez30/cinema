using Domain;

namespace Application.Models
{
    public class SessionBuyTicketSeatModel
    {


        public int Id { get; set; }
        public string Description { get; set; }
        public bool Selected { get; set; }

        public SessionBuyTicketSeatModel()
        {

        }
        
        public SessionBuyTicketSeatModel(Seat seat)
        {
            Id = seat.Id;
            Description = $"{seat.Row} {seat.Column}";
        }

    }
}

