using Autofac;

namespace Windtalker.Plumbing.Auth
{
    [InstancePerLifetimeScope]
    public class CurrentUserProvider : ICurrentUserProvider
    {
        public AuthenticatedUser CurrentUser { get; private set; }

        public void SetCurrentUser(AuthenticatedUser user)
        {
            CurrentUser = user;
        }
    }
}