using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windtalker.Plumbing.Auth
{
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string FailureReason { get; set; }
        public string AuthToken { get; set; }
    }
}