Links to docs:
It is reccomended to read at least first half of topics in this section of documents: https://docs.microsoft.com/en-us/aspnet/core/security/authentication/?view=aspnetcore-5.0

In order to convert your idenity model into the RAW sql script you need to follow these steps.

1. Install following nuget packages into your web project
- Microsoft.AspNetCore.Identity
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.SqlServer

Create a custom database context class derived from identity context. Example:  
```c#
public class ApplicationContext : IdentityDbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }
}
```
Register identity and EF core in your startup class. Example of the default startup class:
```c#
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityScaffold
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
```
Execute the following commands in the console within a directory with the web project `csproj` file
```bash
dotnet ef migrations add CreateIdentitySchema
dotnet ef migrations script --idempotent
```
Copy script from the console, make changes necessary for your case and add it to your `sqlproj` project as a post-deploymnet script of split it into tables.
