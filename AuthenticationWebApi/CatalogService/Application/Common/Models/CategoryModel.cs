using System.Collections.Generic;
using AutoMapper;
using CatalogService.Application.Categories.Queries.GetCategories;
using CatalogService.Application.Common.Mappings;

namespace CatalogService.Application.Common.Models
{
    public class CategoryModel : IMapFrom<CategoryDto>
    {
        public CategoryModel()
        {
            Items = new List<ItemDto>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string ParentCategory { get; set; }

        public IList<ItemDto> Items { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CategoryDto, CategoryModel>();
        }
    }
}
