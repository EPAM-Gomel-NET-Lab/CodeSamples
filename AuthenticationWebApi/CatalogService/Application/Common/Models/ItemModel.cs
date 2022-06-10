using AutoMapper;
using CatalogService.Application.Categories.Queries.GetCategories;
using CatalogService.Application.Common.Mappings;

namespace CatalogService.Application.Common.Models
{
    public class ItemModel : IMapFrom<ItemDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ItemDto, ItemModel>();
        }
    }
}
