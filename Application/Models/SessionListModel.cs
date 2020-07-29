using System;
using System.Collections.Generic;

namespace Application.Models
{
    public class SessionListModel
    {
        public string MovieName { get; set; }
        public DateTime? Date { get; set; }
        public IEnumerable<SessionModel> Sessions { get; set; }
        public bool IsSearch { get; set; }


    }
}

