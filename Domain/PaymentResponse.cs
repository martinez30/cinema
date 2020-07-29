using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class PaymentResponse : BaseEntity
    {
        public PaymentOrder PaymentOrder { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public string NSU { get; set; }

        protected PaymentResponse()
        {

        }

        public PaymentResponse(PaymentOrder paymentOrder, int status, DateTime date, string nSU)
        {
            PaymentOrder = paymentOrder;
            Status = status;
            Date = date;
            NSU = nSU;
        }
    }
}
