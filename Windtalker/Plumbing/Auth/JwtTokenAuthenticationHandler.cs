using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JWT;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace Windtalker.Plumbing.Auth
{
    public class JwtTokenAuthenticationHandler : AuthenticationHandler<JwtTokenAuthenticationOptions>
    {
        private readonly ILogger _logger;

        public JwtTokenAuthenticationHandler(ILogger logger)
        {
            _logger = logger;
        }

        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            string token;

            if (TryRetrieveToken(Request, out token))
            {
                try
                {
                    var secret = Options.SymmetricKey.Replace('-', '+').Replace('_', '/');

                    var claimsIdentityFromToken = ValidateToken(
                        token,
                        Options.Issuer,
                        Options.Audience,
                        secret);

                    return new AuthenticationTicket(claimsIdentityFromToken, new AuthenticationProperties());
                }
                catch (JWT.SignatureVerificationException ex)
                {
                    return null;
                }
                catch (TokenValidationException ex)
                {
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        private ClaimsIdentity ValidateToken(string token,
                                             string issuer,
                                             string audience,
                                             string secretKey,
                                             bool checkExpiration = false)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;

            var authToken = JsonWebToken.DecodeToObject<AuthToken>(token, secretKey);
            if (authToken == null) return null;

            // audience check
            if (!string.IsNullOrEmpty(audience))
            {
                if (!string.Equals(authToken.Audience, audience, StringComparison.Ordinal))
                {
                    throw new TokenValidationException(string.Format("Audience mismatch. Expected: '{0}' and got: '{1}'", audience, authToken.Audience));
                }
            }

            // expiration check
            if (checkExpiration)
            {
                var now = DateTimeOffset.UtcNow;
                var expiresAt = authToken.IssuedAt.AddYears(1);
                if (now > expiresAt)
                {
                    throw new TokenValidationException(string.Format("Token is expired. Expiration: '{0}'. Current: '{1}'", expiresAt, now));
                }
            }

            // issuer check
            if (!string.IsNullOrEmpty(issuer))
            {
                if (!authToken.Issuer.Equals(issuer, StringComparison.Ordinal))
                {
                    throw new TokenValidationException(
                        string.Format("Token issuer mismatch. Expected: '{0}' and got: '{1}'", issuer, authToken.Issuer));
                }
            }

            var claimsIdentity = new ClaimsIdentity("Token");
            claimsIdentity.AddClaims(authToken.Claims.SelectMany(kvp => kvp.Value.Select(v => new Claim(kvp.Key, v))));

            return claimsIdentity;
        }

        private static bool TryRetrieveToken(IOwinRequest request, out string token)
        {
            token = null;
            string[] authzHeaders;
            
            if (request.Headers.TryGetValue("Authorization", out authzHeaders) && authzHeaders.Count() == 1)
            {
                // Remove the bearer token scheme prefix and return the rest as ACS token  
                var bearerToken = authzHeaders.ElementAt(0);
                const string bearerPrefix = "Bearer ";
                token = bearerToken.StartsWith(bearerPrefix) ? bearerToken.Substring(bearerPrefix.Length) : bearerToken;
                return true;
            }

            if (request.Query.Count(q => q.Key == "token") == 1)
            {
                token = request.Query.Single(q => q.Key == "token").Value.First();
                return true;
            }

            // Fail if no Authorization header or more than one Authorization headers  
            // are found in the HTTP request  
            return false;
        }

        public class TokenValidationException : Exception
        {
            public TokenValidationException(string message)
                : base(message)
            {
            }
        }
    }
}