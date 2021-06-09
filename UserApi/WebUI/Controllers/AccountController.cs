using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Client;

namespace WebUI.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserRestClient _userRestClient;

        public AccountController(IUserRestClient userRestClient)
        {
            _userRestClient = userRestClient;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] UserModel user)
        {
            var token = await _userRestClient.Register(user);
            HttpContext.Response.Cookies.Append("secret_jwt_key", token, new CookieOptions 
            { 
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] UserModel user)
        {
            var token = await _userRestClient.Login(user);
            HttpContext.Response.Cookies.Append("secret_jwt_key", token, new CookieOptions 
            { 
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok();
        }
    }
}