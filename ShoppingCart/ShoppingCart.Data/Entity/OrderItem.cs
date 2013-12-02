using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Data.Entity
{
    [Table("OrderItem")]
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int Quantity { get; set; }
        public string VariationXml { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        public int OrderID { get; set; }
        public int ProductID { get; set; }


        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
    }
}
