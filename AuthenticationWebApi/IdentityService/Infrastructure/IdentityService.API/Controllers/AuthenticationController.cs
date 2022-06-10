using IdentityService.Contracts.Models;
using IdentityService.Contracts.Responses;
using IdentityService.Domain.Constants;
using IdentityService.Domain.Entities;
using IdentityService.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Controllers
{
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(UserManager<ApplicationUser> userManager,
            IJwtTokenGenerator jwtTokenGenerator,
            ILogger<AuthenticationController> logger)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationModel user)
        {
            _logger.LogInformation("Trying to register a new user.");

            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Invalid payload was provided.");

                return BadRequest(new RegistrationResponse
                {
                    Result = false,
                    Errors = new List<string>
                    {
                        "Invalid payload"
                    }
                });
            }

            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser != null)
            {
                _logger.LogInformation("The user with provided email already exists.");

                return BadRequest(new RegistrationResponse
                {
                    Result = false,
                    Errors = new List<string>
                    {
                        "Email already exists"
                    }
                });
            }

            var newUser = new ApplicationUser { Email = user.Email, UserName = user.Email };
            var isCreated = await _userManager.CreateAsync(newUser, user.Password);
            if (!isCreated.Succeeded)
            {
                _logger.LogError("Something went wrong while creating a new user.");

                return new JsonResult(new RegistrationResponse
                {
                    Result = false,
                    Errors = isCreated.Errors.Select(x => x.Description).ToList()
                })
                { StatusCode = 500 };
            }

            if (user.IsAdmin)
            {
                _logger.LogInformation($"The user with name {newUser.UserName} was assigned to role Admin.");

                await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
            }

            var roleName = user.IsAdmin ? UserRoles.Admin : UserRoles.Manager;
            var jwtToken = _jwtTokenGenerator.GenerateJwtToken(newUser, roleName);

            _logger.LogInformation($"The user with name {user.Name} was registered.");

            return Ok(new RegistrationResponse
            {
                Result = true,
                Token = jwtToken
            });
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel user)
        {
            _logger.LogInformation($"Trying to login with user email {user.Email}.");

            if (!ModelState.IsValid)
            {
                _logger.LogInformation("Invalid payload was provided.");

                return BadRequest(new RegistrationResponse
                {
                    Result = false,
                    Errors = new List<string>
                    {
                        "Invalid payload"
                    }
                });
            }

            var existingUser = await _userManager.FindByEmailAsync(user.Email);

            if (existingUser == null)
            {
                _logger.LogInformation("The user with provided email had not been found.");

                return BadRequest(new RegistrationResponse
                {
                    Result = false,
                    Errors = new List<string>
                    {
                        "The user with this email does not exist."
                    }
                });
            }

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);
            var roles = await _userManager.GetRolesAsync(existingUser);

            if (!isCorrect)
            {
                _logger.LogInformation($"The provided password is not correct for user {existingUser.UserName}.");

                return BadRequest(new RegistrationResponse
                {
                    Result = false,
                    Errors = new List<string>
                    {
                        "Password is not correct."
                    }
                });
            }

            var isAdmin = roles.Contains(UserRoles.Admin);
            var roleName = isAdmin ? UserRoles.Admin : UserRoles.Manager;
            var jwtToken = _jwtTokenGenerator.GenerateJwtToken(existingUser, roleName);

            _logger.LogInformation($"The user with email {user.Email} was logged in.");

            return Ok(new RegistrationResponse
            {
                Result = true,
                Token = jwtToken
            });
        }
    }
}
