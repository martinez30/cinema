using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Domain
{
    public class PaymentOrder : BaseEntity
    {
        public Card Card { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public IList<PaymentResponse> PaymentResponses { get; set; }


        protected PaymentOrder()
        {
            PaymentResponses = new List<PaymentResponse>();
        }

        public PaymentOrder(Card card, DateTime date, decimal amount) : this()
        {
            Card = card;
            Date = date;
            Amount = amount;
        }
    }
}
