using System.Collections.Generic;
using CatalogService.Application.Common.Mappings;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Categories.Queries.GetCategories
{
    public class CategoryDto : IMapFrom<Category>
    {
        public CategoryDto()
        {
            Items = new List<ItemDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string ParentCategory { get; set; }

        public IList<ItemDto> Items { get; set; }
    }
}
