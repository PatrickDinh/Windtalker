using Autofac;

namespace Windtalker.Domain
{
    public class DomainAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof (Repository<>))
                .As(typeof (IRepository<>))
                .InstancePerLifetimeScope();
        }
    }
}