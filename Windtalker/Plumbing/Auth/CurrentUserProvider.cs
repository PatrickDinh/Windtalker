using System.Security.Claims;
using Autofac;

namespace Windtalker.Plumbing.Auth
{
    [InstancePerLifetimeScope]
    public class CurrentUserProvider : ICurrentUserProvider
    {
        public ClaimsIdentity CurrentUser { get; private set; }

        public void SetCurrentUser(ClaimsIdentity user)
        {
            CurrentUser = user;
        }
    }
}