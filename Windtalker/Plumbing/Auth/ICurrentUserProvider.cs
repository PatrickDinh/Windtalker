using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windtalker.Plumbing.Auth
{
    public interface ICurrentUserProvider
    {
        AuthenticatedUser CurrentUser { get; }

        void SetCurrentUser(AuthenticatedUser user);
    }
}