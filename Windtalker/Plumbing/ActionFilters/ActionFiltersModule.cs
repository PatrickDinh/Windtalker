using Autofac;

namespace Windtalker.Plumbing.ActionFilters
{
    public class ActionFiltersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterAssemblyTypes(ThisAssembly)
                   .AssignableTo<IActionFilter>()
                   .As<IActionFilter>()
                   .InstancePerLifetimeScope();
        }
    }
}