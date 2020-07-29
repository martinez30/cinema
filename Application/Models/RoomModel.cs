using Domain;
using Infra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Application.Models
{
    public class RoomModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Messages.RequiredField)]
        public string Name { get; set; }
        public IList<SeatModel> Seats { get; set; }
       
        [Required(ErrorMessage = Messages.RequiredField)]
        public int? SeatRowCount { get; set; }
        
        [Required(ErrorMessage = Messages.RequiredField)] 
        public int? SeatColumnCount { get; set; }
        public int SeatsCount { get; set; }

        public RoomModel()
        {

        }

        public RoomModel(Room room) 
        {
            Id = room.Id;
            Name = room.Name;
            Seats = new List<SeatModel>();
            foreach (var item in room.Seats)
            {
                Seats.Add(new SeatModel(item));
            }
            SeatsCount = Seats.Count();
        }
    }
}

