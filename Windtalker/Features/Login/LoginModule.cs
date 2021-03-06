﻿using Destructurama.Attributed;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using Serilog;
using Windtalker.Plumbing;
using Windtalker.Plumbing.Auth;

namespace Windtalker.Features.Login
{
    public class LoginModule : NancyModule
    {
        public LoginModule(Authenticator authenticator)
        {
            Post["/login"] = _ =>
            {
                var loginDto = this.Bind<LoginDto>();
                Log.Information("User attempted login {@Request}", loginDto);
                var authResult = authenticator.Authenticate(loginDto.Email, loginDto.Password);
                if (authResult.Success)
                {
                   return new JsonObjectResponse(new LoginResponse(authResult.AuthToken));
                }
                else
                {
                    return ErrorResponse.FromMessage(authResult.FailureReason, HttpStatusCode.Unauthorized);
                }
            };
        }

        private class LoginResponse
        {
            public string AuthToken { get; set; }

            public LoginResponse(string authToken)
            {
                AuthToken = authToken;
            }
        }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        [NotLogged]
        public string Password { get; set; }
    }
}