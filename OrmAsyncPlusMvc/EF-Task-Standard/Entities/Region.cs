using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Task_Standard.Entities
{
    [Table("Regions")]
    public class Region
    {
        [Key]
        public int RegionID { get; set; }

        [Required]
        public string RegionDescription { get; set; }
    }
}
