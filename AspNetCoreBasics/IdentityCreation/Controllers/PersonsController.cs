using ClientApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;

namespace IdentityCreation.Controllers
{
    public class PersonsController : Controller
    {
        private readonly IFunctor _functor;

        public PersonsController(IFunctor functor)
        {
            _functor = functor;
        }

        [AllowAnonymous]
        [HttpGet("person")]
        public IActionResult Index1([FromServices]IOptions<PersonSettings> settings)
        {
            throw new System.Exception("oops something went wrong!");
            return Content($"{settings.Value.BankAccountName}, {settings.Value.Country}");
        }

        [HttpGet("person1")]
        public IActionResult Index()
        {
            var person = new Person("John", "Doe");
            return Content($"{person.FirstName} {person.LastName}");
        }

        public record Person(string FirstName, string LastName);

        [Authorize]
        [HttpGet("person2")]
        public IActionResult Index2([FromServices]IOptions<PersonSettings> settings)
        {
            return Content($"{settings.Value.BankAccountName}, {settings.Value.Country}");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
