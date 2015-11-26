using Nancy;
using Nancy.Security;
using Windtalker.Plumbing;
using Windtalker.Plumbing.Auth;

namespace Windtalker.Features.ManageUser
{
    public class CurrentUserModule : NancyModule
    {
        public CurrentUserModule(ICurrentUserProvider currentUserProvider)
        {
            this.RequiresMSOwinAuthentication();

            Get["/currentUser"] = _ => new JsonObjectResponse(currentUserProvider.CurrentUser);
        }
    }
}