using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Task_Standard.Entities
{
    [Table("Shippers")]
    public class Shipper
    {
        [Key]
        public int ShipperID { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string Phone { get; set; }
    }
}
