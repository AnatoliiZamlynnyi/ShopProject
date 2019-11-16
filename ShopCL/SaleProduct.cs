using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShopCL
{
    public class SaleProduct
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string ProductName { get; set; }
        public double PriceIn { get; set; }
        public double Count { get; set; }
        public string Unit { get; set; }
        public double PriceOut { get; set; }
        public double Summa { get; set; }
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category Categoryes { get; set; }
        public int SupplierID { get; set; }
        [ForeignKey("SupplierID")]
        public virtual Supplier Suppliers { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User Users { get; set; }
        public string Note { get; set; }
    }
}