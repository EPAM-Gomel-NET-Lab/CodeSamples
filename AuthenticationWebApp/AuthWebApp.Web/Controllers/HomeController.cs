using AuthWebApp.Web.Data;
using AuthWebApp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthWebApp.Web
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Details()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return View(new UserIdentityDetailsViewModel
            {
                Claims = User.Claims.ToList(),
                UserId = user.Id,
                UserName = User.Identity.Name,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Roles = (List<string>)await _userManager.GetRolesAsync(user)
            });
        }
    }
}
