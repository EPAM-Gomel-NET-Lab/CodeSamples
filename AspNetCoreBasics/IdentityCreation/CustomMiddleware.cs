using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IdentityCreation
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public CustomMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("Hello before!");
            await _requestDelegate.Invoke(context);
            await context.Response.WriteAsync("Hello after!");
        }
    }
}
