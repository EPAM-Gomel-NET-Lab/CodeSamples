using IdentityCreation;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Net.Http;

namespace AspNetTesting
{
    public class IntegrationTestBase
    {
        [SetUp]
        public void Setup()
        {
            var build = WebHost.CreateDefaultBuilder().UseEnvironment("testing").UseStartup<Startup>();
            var testServer = new TestServer(build);
            Client = testServer.CreateClient();
            Provider = testServer.Host.Services.CreateScope().ServiceProvider;
        }

        public HttpClient Client { get; private set; }

        public IServiceProvider Provider { get; private set; }
    }
}
