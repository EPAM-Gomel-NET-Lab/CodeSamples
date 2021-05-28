using ClassicTemplate.Models;
using ClassicTemplate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ClassicTemplate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _personService;

        public HomeController(ILogger<HomeController> logger, IUserService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        public IActionResult Index()
        {
            return View(new User("John", "Doe"));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("persons")]
        public IActionResult Persons()
        {
            return View(_personService.GetAll());
        }

        [HttpGet("persons/add")]
        public IActionResult CreatePerson() => View();

        [HttpPost("persons/add")]
        public IActionResult CreatePerson(User person) 
        {
            _personService.Add(person);
            return RedirectToAction(nameof(Persons));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
