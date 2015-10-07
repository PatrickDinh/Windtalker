using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windtalker.Domain.Exceptions
{
    public class EmailAddressAlreadyTakenException : DomainException
    {
        public EmailAddressAlreadyTakenException()
        {
        }

        public EmailAddressAlreadyTakenException(string message) : base(message)
        {
        }

        public EmailAddressAlreadyTakenException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}