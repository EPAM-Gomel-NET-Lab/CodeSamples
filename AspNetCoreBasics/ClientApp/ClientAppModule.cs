using Microsoft.Extensions.DependencyInjection;
using System;

namespace ClientApp
{
    public static class ClientAppModule
    {
        public static IServiceCollection AddClientApp(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<IFunctor, Functor>(); //// new Functor()
            services.AddScoped<IFunctor, Functor>(); /// new Functor() only 1 time pet scope
            services.AddSingleton<IFunctor, Functor>(); /// new Functor() only 1 time
            
            ////IServiceProvider container = services.BuildServiceProvider();
            ///.ServiceProvider.GetService<IFunctor>();

            return services;
        }
    }
}
