using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using EF_Task.Scope;

namespace AsyncVsSyncExample
{
    public static class ResolverConfig
    {
        public static void RegisterResolver()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterInstance(RouteTable.Routes).As<IEnumerable<RouteBase>>();
            builder.RegisterModule<DbScopeModule>();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
