using Domain;
using Infra;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class SeatModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Messages.RequiredField)]
        public int RoomId { get; set; }

        [Required(ErrorMessage = Messages.RequiredField)]
        public string Row { get; set; }

        [Required(ErrorMessage = Messages.RequiredField)]
        public int Column { get; set; }
        public bool Avaliable { get; set; }

        public SeatModel()
        {

        }

        public SeatModel(string row, int column)
        {
            Row = row;
            Column = column;
        }

        public SeatModel(Seat seat)
        {
            Id = seat.Id;
            RoomId = seat.Room.Id;
            Row = seat.Row;
            Column = seat.Column;
            Avaliable = false;
        }
    }
}

