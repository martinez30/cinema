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
        public int? Rows { get; set; }
        public int? Columns{ get; set; }

        public SessionBuyTicketModel()
        {
                
        }

        public SessionBuyTicketModel(Session session, IList<Seat> seats, IList<Ticket> tickets)
        {
            var a = seats.Select(x=> x.Row);
            a = a.Distinct();
            Rows = a.Count();
            var b = seats.Select(x => x.Column);
            b = b.Distinct();
            Columns = b.Count();
            Id = session.Id;
            MovieName = session.Movie.Name;
            Date = session.Date.ToString("dd/MM/yyyy HH:mm");
            Seats = seats.Select(x => new SessionBuyTicketSeatModel(x, tickets)).OrderByDescending(x => x.Row).ThenBy(x => x.Column).ToList();
        }
        public SessionBuyTicketModel(Session session, IList<Seat> seats)
        {
            Id = session.Id;
            MovieName = session.Movie.Name;
            Date = session.Date.ToString("dd/MM/yyyy HH:mm");
        }
    }
}

