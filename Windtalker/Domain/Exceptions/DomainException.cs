﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windtalker.Domain.Exceptions
{
    [Serializable]
    public class DomainException : Exception
    {
        public DomainException()
        {
        }

        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}