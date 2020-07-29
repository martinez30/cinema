using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{

    public class Client : BaseEntity
    {
        public long CPF { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public IList<Order> Orders { get; set; }
        public IList<Card> Cards { get; set; }
        protected Client()
        {
            Orders = new List<Order>();
            Cards = new List<Card>();
        }

        public Client(long cpf, string firstName, string lastName, DateTime dateOfBirth) : this ()
        {
            CPF = cpf;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }
    }
}
