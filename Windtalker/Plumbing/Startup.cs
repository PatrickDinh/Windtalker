﻿using System.Configuration;
using Autofac;
using Autofac.Integration.SignalR;
using AutofacSerilogIntegration;
using Destructurama;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Extensions;
using Nancy.Json;
using Nancy.Owin;
using Owin;
using Serilog;
using Windtalker.Domain;
using Windtalker.Features.ManageRoom;
using Windtalker.Features.Register;
using Windtalker.Plumbing;
using Windtalker.Plumbing.Auth;
using Windtalker.Plumbing.Logging;
using Windtalker.Settings;

[assembly: OwinStartup(typeof (Startup))]

namespace Windtalker.Plumbing
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureLogger();

            var builder = new ContainerBuilder();
            builder.RegisterLogger();
            builder.RegisterByAttributes(typeof (Startup).Assembly);
            builder.RegisterAssemblyModules(typeof (Startup).Assembly);
            builder.RegisterHubs(typeof (Startup).Assembly);
            var container = builder.Build();

            // config hubs
            var config = new HubConfiguration();
            config.Resolver = new AutofacDependencyResolver(container);

            app.UseJwtTokenAuthentication(container.Resolve<IssuerSetting>(),
                                          container.Resolve<AudienceSetting>(),
                                          container.Resolve<ClientSecretSetting>(),
                                          container.Resolve<ICurrentUserProvider>());

            app.MapSignalR(config);

            app.UseNancy(new NancyOptions
            {
                Bootstrapper = new NancyBootstrapper(container)
            });
            app.UseStageMarker(PipelineStage.MapHandler);
            app.UseCors(CorsOptions.AllowAll);

            JsonSettings.RetainCasing = false;

            SeedData(container);
        }

        private void ConfigureLogger()
        {
            var seqUrl = ConfigurationManager.AppSettings["SeqUrl"];

            Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
                                                  .Destructure.With<JObjectDestructuringPolicy>()
                                                  .Destructure.UsingAttributes()
                                                  .WriteTo.Seq(seqUrl)
                                                  .CreateLogger();
        }

        private void SeedData(IContainer container)
        {
            var createUser = container.Resolve<CreateUser>();
            var createRoom = container.Resolve<CreateRoom>();
            var unitOfWork = container.Resolve<IUnitOfWork>();

            createUser.Create("a@example.com", "123123");
            createUser.Create("b@example.com", "123123");

            createRoom.Create("General");
            createRoom.Create("Room 2");

            unitOfWork.SaveChanges();
        }
    }
}