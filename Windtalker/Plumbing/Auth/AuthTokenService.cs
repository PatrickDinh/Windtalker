using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using JWT;
using Windtalker.Settings;

namespace Windtalker.Plumbing.Auth
{
    public class AuthTokenService
    {
        private readonly AudienceSetting _audience;
        private readonly ClientSecretSetting _clientSecret;
        private readonly IClock _clock;
        private readonly IssuerSetting _issuer;

        public AuthTokenService(IClock clock,
                                ClientSecretSetting clientSecret,
                                AudienceSetting audience,
                                IssuerSetting issuer)
        {
            _clock = clock;
            _clientSecret = clientSecret;
            _audience = audience;
            _issuer = issuer;
        }

        public string GenerateAuthToken(Guid personId, IEnumerable<Claim> claims)
        {
            var mappedClaims = claims
                .GroupBy(c => c.Type)
                .ToDictionary(g => g.Key, g => g.Select(c => c.Value).ToArray());

            var token = new AuthToken
            {
                IssuedAt = _clock.UtcNow,
                Claims = mappedClaims,
                Issuer = _issuer,
                Audience = _audience
            };

            return JsonWebToken.Encode(token, _clientSecret, JwtHashAlgorithm.HS256);
        }
    }
}