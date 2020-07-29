using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Room : BaseEntity
    {
        public string Name { get; set; }
        public IList<Seat> Seats { get; set; }
        public IList<Session> Sessions { get; set; }
        protected Room()
        {
            Seats = new List<Seat>();
            Sessions = new List<Session>();
        }

        public Room(string name) : this()
        {
            Name = name;
        }
    }
}
