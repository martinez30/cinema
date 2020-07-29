using Domain;
using Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class TicketModel
    {
        [Required(ErrorMessage = Messages.RequiredField)]
        public int SessionId { get; set; }
     
        [Required(ErrorMessage = Messages.RequiredField)]
        public int SeatId { get; set; }


        public TicketModel(Ticket ticket) 
        {
            SessionId = ticket.Session.Id;
            SeatId = ticket.Seat.Id;
        }
    }
}

