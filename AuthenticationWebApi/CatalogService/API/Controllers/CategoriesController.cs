using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CatalogService.Application.Categories.Commands.CreateCategory;
using CatalogService.Application.Categories.Commands.DeleteCategory;
using CatalogService.Application.Categories.Commands.UpdateCategory;
using CatalogService.Application.Categories.Queries.GetCategories;
using CatalogService.Application.Categories.Queries.GetCategory;
using CatalogService.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CatalogService.API.Controllers
{
    [Authorize]
    public class CategoriesController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(IMapper mapper,
            ILogger<CategoriesController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryModel>>> GetAll()
        {
            _logger.LogInformation("Getting the list of all categories.");

            var categories = await Mediator.Send(new GetCategoriesQuery());

            var categoriesList = new List<CategoryModel>();

            foreach (var category in categories)
            {
                var categoryModel = _mapper.Map<CategoryModel>(category);

                categoriesList.Add(categoryModel);
            }

            _logger.LogInformation("Returning the list of all categories.");

            return categoriesList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> Get(int id)
        {
            _logger.LogInformation($"Getting the the category with id = {id}.");

            var category = await Mediator.Send(new GetCategoryQuery() { Id = id });

            var categoryModel = _mapper.Map<CategoryModel>(category);

            _logger.LogInformation($"Returning the the category with id = {id}.");

            return categoryModel;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> Create(CreateCategoryCommand command)
        {
            _logger.LogInformation($"Trying to create a new category with name {command.Name}.");

            var result = await Mediator.Send(command);

            _logger.LogInformation($"A new category with name {command.Name} was created.");

            return result;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(int id, UpdateCategoryCommand command)
        {
            _logger.LogInformation($"Trying to update the category with name {command.Name}.");

            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            _logger.LogInformation($"The category with name {command.Name} was updated.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation($"Trying to delete a category with id {id}.");

            await Mediator.Send(new DeleteCategoryCommand { Id = id });

            _logger.LogInformation($"The category with id {id} was deleted.");

            return NoContent();
        }
    }
}
