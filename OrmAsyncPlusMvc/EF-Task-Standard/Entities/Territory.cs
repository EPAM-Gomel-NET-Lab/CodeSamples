using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Task_Standard.Entities
{
    [Table("Territories")]
    public class Territory
    {
        [Key]
        public int TerritoryID { get; set; }

        [Required]
        public string TerritoryDescription { get; set; }

        public int RegionID { get; set; }
    }
}
