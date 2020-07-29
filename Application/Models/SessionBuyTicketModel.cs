using Domain;
using Infra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Application.Models
{

    public class SessionBuyTicketModel
    {
        public int Id { get; set; }

        public string MovieName { get; set; }
        public string Date { get; set; }
        public List<SessionBuyTicketSeatModel> Seats { get; set; }

        public SessionBuyTicketModel()
        {
                
        }

        public SessionBuyTicketModel(Session session, IList<Seat> seats)
        {
            Id = session.Id;
            MovieName = session.Movie.Name;
            Date = session.Date.ToString("dd/MM/yyyy HH:mm");
            Seats = seats.Select(x => new SessionBuyTicketSeatModel(x)).OrderBy(x=> x.Description).ToList();
        }
    }
}

