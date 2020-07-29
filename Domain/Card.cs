using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Card : BaseEntity
    {
        public Client Client { get; set; }
        public string Token { get; set; }
        public string Brand { get; set; }
        public string LastFour { get; set; }
        public IList<PaymentOrder> PaymentOrders { get; set; }

        protected Card()
        {
            PaymentOrders = new List<PaymentOrder>();
        }

        public Card(Client client, string token, string brand, string lastFour) : this()
        {
            Client = client;
            Token = token;
            Brand = brand;
            LastFour = lastFour;
        }
    }
}
