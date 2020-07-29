using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Domain
{
    public class DescontType : BaseEntity
    {
        public string Name { get; set; }
        public decimal Percentage { get; set; }

    }
}
