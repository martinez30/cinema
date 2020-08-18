using Domain;
using Infra;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class SessionModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Messages.RequiredField)]
        public int MovieId { get; set; }

        [Required(ErrorMessage = Messages.RequiredField)]
        public int RoomId { get; set; }

        [Required(ErrorMessage = Messages.RequiredField)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = Messages.RequiredField)]
        public decimal Amount { get; set; }
        
        [Required(ErrorMessage = Messages.RequiredField)]
        public bool DescontAllowed { get; set; }
        public string MovieName { get; set; }
        public string RoomName { get; set; }
        public int AvaliableSeatCount { get; set; }


        public SessionModel()
        {

        }

        public SessionModel(Session session)
        {
            Id = session.Id;
            MovieId = session.Movie.Id;
            MovieName = session.Movie.Name;
            RoomId = session.Room.Id;
            RoomName = session.Room.Name;
            Date = session.Date;
            Amount = session.Amount;
            DescontAllowed = session.DescontAllowed;
        }
    }
}

