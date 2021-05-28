using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Reflection;

namespace AspNetTesting
{
    public class IntegrationTestBase
    {
        private TestServer _testServer;

        [SetUp]
        public void Setup()
        {
            var build = WebHost.CreateDefaultBuilder().UseEnvironment("testing").UseStartup<TestStartup>();
            _testServer = new TestServer(build);
            Client = _testServer.CreateClient();
            Provider = _testServer.Host.Services.CreateScope().ServiceProvider;
        }

        protected HttpClient Client { get; private set; }

        protected IServiceProvider Provider { get; private set; }

        protected T GetService<T>() where T : class => Provider.GetService(typeof(T)) as T;

        private class TestStartup : ClassicTemplate.Startup
        {
            public TestStartup(IConfiguration configuration)
                : base(configuration)
            {
            }

            public override void ConfigureServices(IServiceCollection services)
            {
                base.ConfigureServices(services);
                services.AddControllersWithViews().AddApplicationPart(Assembly.Load(new AssemblyName("ClassicTemplate")));
            }
        }
    }
}
