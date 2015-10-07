using Autofac;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Extensions;
using Nancy.Json;
using Nancy.Owin;
using Owin;
using Windtalker.Domain;
using Windtalker.Features.Register;
using Windtalker.Plumbing;
using Windtalker.Plumbing.Auth;
using Windtalker.Settings;

[assembly: OwinStartup(typeof (Startup))]

namespace Windtalker.Plumbing
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterByAttributes(typeof (Startup).Assembly);
            builder.RegisterAssemblyModules(typeof (Startup).Assembly);
            var container = builder.Build();

            app.UseJwtTokenAuthentication(container.Resolve<IssuerSetting>(),
                                          container.Resolve<AudienceSetting>(),
                                          container.Resolve<ClientSecretSetting>());
            app.MapSignalR();
            app.UseNancy(new NancyOptions
            {
                Bootstrapper = new NancyBootstrapper(container)
            });
            app.UseStageMarker(PipelineStage.MapHandler);
            app.UseCors(CorsOptions.AllowAll);

            JsonSettings.RetainCasing = false;

            SeedData(container);
        }

        private void SeedData(IContainer container)
        {
            var createUser = container.Resolve<CreateUser>();
            var unitOfWork = container.Resolve<IUnitOfWork>();

            createUser.Create("a@example.com", "123123");
            createUser.Create("b@example.com", "123123");

            unitOfWork.SaveChanges();
        }
    }
}