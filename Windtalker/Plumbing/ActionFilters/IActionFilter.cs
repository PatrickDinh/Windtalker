using Nancy;
using Nancy.Bootstrapper;

namespace Windtalker.Plumbing.ActionFilters
{
    public interface IActionFilter
    {
        void Enrol(IPipelines pipelines, NancyContext context);
    }
}