using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Task_Standard.Entities
{
    [Table("CreditCards")]
    public class CreditCard
    {
        [Key]
        public int CreditCardID { get; set; }

        public string CardNumber { get; set; }

        public DateTime ExpiryDate { get; set; }

        public int EmployeeID { get; set; }
    }
}
