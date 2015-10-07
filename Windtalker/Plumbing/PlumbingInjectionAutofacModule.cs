using Autofac;
using Windtalker.Plumbing.Auth;

namespace Windtalker.Plumbing
{
    public class PlumbingInjectionAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthTokenService>()
                   .AsSelf()
                   .SingleInstance();
        }
    }
}