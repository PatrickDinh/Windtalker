using System;
using System.Collections.Generic;

namespace Windtalker.Plumbing.Auth
{
    public class AuthToken
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public DateTimeOffset IssuedAt { get; set; }
        public Dictionary<string, string[]> Claims { get; set; }
    }
}