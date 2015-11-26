using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.Infrastructure;
using Owin;

namespace Windtalker.Plumbing.Auth
{
    public class JwtTokenAuthenticationMiddleware : AuthenticationMiddleware<JwtTokenAuthenticationOptions>
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ILogger _logger;

        public JwtTokenAuthenticationMiddleware(OwinMiddleware next,
                                                IAppBuilder app,
                                                JwtTokenAuthenticationOptions options,
                                                ICurrentUserProvider currentUserProvider)
            : base(next, options)
        {
            _currentUserProvider = currentUserProvider;
            _logger = app.CreateLogger<JwtTokenAuthenticationMiddleware>();
        }

        protected override AuthenticationHandler<JwtTokenAuthenticationOptions> CreateHandler()
        {
            return new JwtTokenAuthenticationHandler(_logger, _currentUserProvider);
        }
    }
}