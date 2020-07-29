using System;

namespace Application
{
    public class DuplicatedExcpection : Exception
    {
        public DuplicatedExcpection(string message) : base(message)
        {
        }
    }
}
