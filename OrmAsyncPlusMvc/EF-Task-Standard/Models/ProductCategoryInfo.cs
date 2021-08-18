namespace EF_Task_Standard.Models
{
    public class ProductCategoryInfo
    {
        public int OrderID { get; set; }
        public string ProductName { get; set; }
        public string ContactName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
    }
}
