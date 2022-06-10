using System.Collections.Generic;

namespace CatalogService.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string ParentCategory { get; set; }

        public List<Item> Items { get; set; }
    }
}
