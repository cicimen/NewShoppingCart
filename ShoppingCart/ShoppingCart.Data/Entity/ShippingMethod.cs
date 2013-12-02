using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ShoppingCart.Data.Entity
{
    [Table("ShippingMethod")]
    public class ShippingMethod
    {
        public int ShippingMethodID { get; set; }

        [MaxLength(300)]
        public string ShippingMethodName { get; set; }

        [MaxLength(300)]
        public string ShippingMethodDescription { get; set; }

        public decimal ShippingMethodPrice { get; set; }

        public bool Enabled { get; set; }

        public int ShippingMethodDisplayOrder { get; set; }

    }
}
