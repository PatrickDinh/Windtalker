using System;
using System.Collections.Generic;
using System.IO;
using Autofac;
using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Conventions;
using ThirdDrawer.Extensions.CollectionExtensionMethods;
using Windtalker.Domain;
using Windtalker.Plumbing.ActionFilters;
using Windtalker.Plumbing.Auth;

namespace Windtalker.Plumbing
{
    public class NancyBootstrapper : AutofacNancyBootstrapper
    {
        private readonly ILifetimeScope _container;

        public NancyBootstrapper(ILifetimeScope container)
        {
            _container = container;
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            var authConfiguration = new StatelessAuthenticationConfiguration(container.Resolve<Authenticator>().GetUserIdentity);
            StatelessAuthentication.Enable(pipelines, authConfiguration);
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            container.Resolve<IEnumerable<IActionFilter>>()
                     .Do(f => f.Enrol(pipelines, context))
                     .Done();

            pipelines.AfterRequest.AddItemToStartOfPipeline(c => CommitUnitOfWork(container, context, c));
            base.RequestStartup(container, pipelines, context);
        }

        private void CommitUnitOfWork(ILifetimeScope container, NancyContext context, NancyContext nancyContext)
        {
            try
            {
                if (context.Request.Method != "GET" && context.Response.StatusCode == HttpStatusCode.OK)
                {
                    container.Resolve<IUnitOfWork>().SaveChanges();
                }
            }
            catch (Exception ex)
            {
                nancyContext.Response = ErrorResponse.FromMessage("Exception committing unit of work",
                    HttpStatusCode.InternalServerError);
            }
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);
            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("bower_components"));
            conventions.StaticContentsConventions.AddDirectory("build", null , new[]{
                ".js", ".ts", ".map",
                ".css",
                ".html",
                ".png", ".jpg", ".gif",
                ".ttf", ".woff", ".woff2", ".eot",".svg" });
        }

        protected override ILifetimeScope GetApplicationContainer()
        {
            return _container;
        }
    }

}
