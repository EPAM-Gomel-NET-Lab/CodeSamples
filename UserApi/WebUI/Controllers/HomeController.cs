using CityApiClientGenerated;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly CityApiClient _cityApiClient;

        public HomeController(CityApiClient cityApiClient)
        {
            _cityApiClient = cityApiClient;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var cities = await _cityApiClient.CitiesAllAsync();
            return View(cities);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
