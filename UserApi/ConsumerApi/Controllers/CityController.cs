using System.Collections.Generic;
using ConsumerApi.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerApi.Controllers
{
    /// <summary>
    /// Resource for the operations against the city entity
    /// </summary>
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [Produces("application/json")]
    public class CitiesController : ControllerBase
    {
        private static readonly List<City> _cities = new List<City>(City.BelarusRegionalCities);

        /// <summary>
        /// Returns list of the cities
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<City>), StatusCodes.Status200OK)]
        public IActionResult GetCities()
        {
            return Ok(_cities);
        }

        /// <summary>
        /// Returns list of the cities
        /// </summary>
        /// <returns></returns>
        [HttpGet("{name}")]
        [ProducesResponseType(typeof(City), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCityByName(string name)
        {
            var cityWithName = _cities.GetCityByName(name);
            IActionResult result = cityWithName is null ? NotFound() : Ok(cityWithName);
            return result;
        }

        /// <summary>
        /// Returns list of the cities
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddCity([FromBody] City city)
        {
            _cities.Add(city);
            return CreatedAtAction(nameof(GetCityByName), new { name = city.Name }, city);
        }

        /// <summary>
        /// Returns list of the cities
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update([FromBody] City city)
        {
            var cityByName = _cities.GetCityByName(city.Name);
            if (cityByName != null)
            {
                _cities.Remove(cityByName);
                _cities.Add(city);
                return Ok();
            }

            _cities.Add(city);
            return Ok();
        }

        /// <summary>
        /// Returns list of the cities
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{name}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCityByName(string name)
        {
            var cityWithName = _cities.GetCityByName(name);
            _cities.Remove(cityWithName);
            return NoContent();
        }
    }
}
