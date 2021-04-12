using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebApp.Controllers
{
    public class CustomActionResultsController : Controller
    {
        public async void Index()
        {
            HttpContext.Response.StatusCode = 200;
            HttpContext.Response.ContentType = "text/html";
            var content = Encoding.ASCII.GetBytes($"<html><body><h1>Hello World</h1></body></html>");
            await HttpContext.Response.Body.WriteAsync(content, 0, content.Length);
        }

        public IActionResult Normal()
        {
            return View();
        }
    }
}
