using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Autofac;
using Nancy;
using Nancy.Security;
using Windtalker.Domain.Queries.Users;

namespace Windtalker.Plumbing.Auth
{
    [SingleInstance]
    public class Authenticator
    {
        public const string AuthTokenKey = "AuthToken";
        private readonly AuthTokenService _authTokenService;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IQueryExecutor _queryExecutor;

        public Authenticator(AuthTokenService authTokenService,
                             IQueryExecutor queryExecutor,
                             IPasswordHashingService passwordHashingService)
        {
            _authTokenService = authTokenService;
            _queryExecutor = queryExecutor;
            _passwordHashingService = passwordHashingService;
        }

        public AuthenticationResult Authenticate(string email, string password)
        {
            var query = new FindUserByEmailQuery(email);
            var user = _queryExecutor.Execute(query).FirstOrDefault();
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    FailureReason = "Email address is unknown"
                };
            }

            var isPasswordCorrect = _passwordHashingService.TryVerify(password, user.HashedPassword);
            if (!isPasswordCorrect)
            {
                return new AuthenticationResult
                {
                    Success = false,
                    FailureReason = "Password is incorrect"
                };
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var token = _authTokenService.GenerateAuthToken(user.Id, claims);

            return new AuthenticationResult
            {
                Success = true,
                AuthToken = token
            };
        }

        public IUserIdentity GetUserIdentity(NancyContext context)
        {
            var token = context.Request.Headers.Authorization;
            if (string.IsNullOrWhiteSpace(token)) return null;
            return _authTokenService.GetUserFromToken(token);
        }
    }
}