using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Nancy.Security;

namespace Windtalker.Plumbing.Auth
{
    public class AuthenticatedUser : IUserIdentity
    {
        private readonly Dictionary<string, string[]> _claims;
        private readonly Guid _userId;

        public AuthenticatedUser(Guid userId, Dictionary<string, string[]> claims)
        {
            _userId = userId;
            _claims = claims;
        }

        public IEnumerable<Claim> FlattenedClaims
        {
            get
            {
                return _claims
                    .SelectMany(kvp => kvp.Value.Select(v => new Claim(kvp.Key, v)))
                    .ToArray();
            }
        }

        public string UserName => _userId.ToString();

        public IEnumerable<string> Claims => FlattenedClaims
            .Select(MapClaim)
            .ToArray();

        public static string MapClaim(Claim c)
        {
            return $"{c.Type}:{c.Value}";
        }
    }
}