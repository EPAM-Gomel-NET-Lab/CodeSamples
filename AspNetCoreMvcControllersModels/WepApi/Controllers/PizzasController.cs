using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WepApi.Controllers
{
    public record Pizza([Required]string Name, [Range(1, 999.99)]decimal Cost, [StringLength(15)]string Note);

    [ApiController]
    [Route("[controller]")]
    public class PizzasController : ControllerBase
    {
        [HttpPost]
        public IActionResult MakePizza(Pizza pizza)
        {
            // do some business logic related to saving

            return Created("path to get by id", new { id = 9 });
        }

        // it's just the same
        [HttpPost("MakePizza")]
        public IActionResult MakePizza2([FromBody] Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // do some business logic related to saving

            return Created("path to get by id", new { id = 9 });
        }

        [HttpGet("{id}")]
        public IActionResult GetPizzaById(int id)
        {
            // do some business logic related to finding entity by id

            return Ok();
        }

        [HttpPost("files")]
        public IActionResult UploadPizzaPicture(IFormFile file)
        {
            // do some business logic related to file saving

            return Created("path to file by id", new { id = 8 });
        }
    }
}
