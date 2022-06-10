using AutoMapper;
using CatalogService.Application.Common.Mappings;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Categories.Queries.GetCategories
{
    public class ItemDto : IMapFrom<Item>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Item, ItemDto>();
        }
    }
}
