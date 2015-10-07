using Nancy;

namespace Windtalker.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = p => View[@"build/index"];
        }
    }
}