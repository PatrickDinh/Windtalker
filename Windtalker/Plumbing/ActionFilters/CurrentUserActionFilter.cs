using Nancy;
using Nancy.Bootstrapper;
using Windtalker.Plumbing.Auth;

namespace Windtalker.Plumbing.ActionFilters
{
    public class CurrentUserActionFilter : IActionFilter
    {
        private readonly ICurrentUserProvider _currentUserProvider;

        public CurrentUserActionFilter(ICurrentUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider;
        }

        public void Enrol(IPipelines pipelines, NancyContext context)
        {
            pipelines.BeforeRequest.AddItemToEndOfPipeline(ctx => BeforeRequest(context));
        }

        private Response BeforeRequest(NancyContext context)
        {
            var currentUser = context.CurrentUser as AuthenticatedUser;
            if (currentUser != null)
            {
                _currentUserProvider.SetCurrentUser(currentUser);
            }

            return context.Response;
        }
    }
}