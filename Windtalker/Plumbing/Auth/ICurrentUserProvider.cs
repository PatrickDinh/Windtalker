using System.Security.Claims;

namespace Windtalker.Plumbing.Auth
{
    public interface ICurrentUserProvider
    {
        ClaimsIdentity CurrentUser { get; }

        void SetCurrentUser(ClaimsIdentity user);
    }
}