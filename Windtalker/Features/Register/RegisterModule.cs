using Nancy;
using Nancy.ModelBinding;
using Windtalker.Domain.Exceptions;
using Windtalker.Plumbing;

namespace Windtalker.Features.Register
{
    public class RegisterModule : NancyModule
    {
        public RegisterModule(ICreateUser createUser)
        {
            Post["/register"] = _ =>
            {
                var dto = this.Bind<RegisterDto>();

                try
                {
                    createUser.Create(dto.Email, dto.Password);
                    return new Response().WithStatusCode(HttpStatusCode.OK);
                }
                catch (EmailAddressAlreadyTakenException)
                {
                    return ErrorResponse.FromMessage("Email address already taken", HttpStatusCode.BadRequest);
                }
            };
        }
    }
}