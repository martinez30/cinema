using System;
using System.Security.Cryptography.X509Certificates;

namespace Domain
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? ModifiedDate { get; private set; }
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }
        public void SetModifiedDate(DateTime date)
        {
            ModifiedDate = date;
        }
    }
}
