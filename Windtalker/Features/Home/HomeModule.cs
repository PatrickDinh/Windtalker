using Nancy;

namespace Windtalker.Features.Home
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = p => View[@"build/index"];
        }
    }
}