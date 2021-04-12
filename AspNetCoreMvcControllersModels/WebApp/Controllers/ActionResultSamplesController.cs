using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApp.Controllers
{
    public class ActionResultSamplesController : Controller
    {
        public IActionResult Index()
        {
            // some business logic goes here

            return View();
        }

        public IActionResult JsonObject()
        {
            return Json(new { Id = 4, Name = "John Smith" });
        }

        public IActionResult Redirect()
        {
            // some business logic goes here

            if (new Random().Next(5) % 2 == 0)
            {
                return Json(new { Id = 77, Name = "Kate Taylor" });
            }

            return RedirectToAction(nameof(JsonObject));
        }

        public IActionResult File()
        {
            var file = System.IO.File.ReadAllBytes("wwwroot/images/pepperoni.png");
            return File(file, "application/octet-stream", "pepperoni.png");
        }
    }
}
