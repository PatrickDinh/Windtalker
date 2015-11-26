using System;
using Microsoft.Owin.Extensions;
using Owin;

namespace Windtalker.Plumbing.Auth
{
    public static class JwtTokenAuthenticationExtensions
    {
        public static IAppBuilder UseJwtTokenAuthentication(this IAppBuilder app,
                                                            JwtTokenAuthenticationOptions options,
                                                            ICurrentUserProvider currentUserProvider)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            app.Use(typeof (JwtTokenAuthenticationMiddleware), app, options, currentUserProvider);
            app.UseStageMarker(PipelineStage.Authenticate);
            return app;
        }

        public static IAppBuilder UseJwtTokenAuthentication(this IAppBuilder app,
                                                            string issuer,
                                                            string audience,
                                                            string clientSecret,
                                                            ICurrentUserProvider currentUserProvider)
        {
            return app.UseJwtTokenAuthentication(new JwtTokenAuthenticationOptions(issuer, audience, clientSecret), currentUserProvider);
        }
    }
}