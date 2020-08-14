using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Domain
{
    public class Seat : BaseEntity
    {
        public Room Room { get; set; }
        public string Row { get; set; }
        public int Column { get; set; }

        protected Seat()
        {

        }

        public Seat(Room room, string row, int column)
        {
            Room = room;
            Row = row;
            Column = column;
        }
    }
}
