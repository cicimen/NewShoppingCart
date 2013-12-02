using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Data.Entity
{
    [Table("PaymentMethod")]
    public class PaymentMethod
    {
        public int PaymentMethodID { get; set; }

        [MaxLength(300)]
        public string PaymentMethodName { get; set; }

        [MaxLength(300)]
        public string PaymentMethodDescription { get; set; }

        public bool Enabled { get; set; }

        public int PaymentMethodDisplayOrder { get; set; }
    }
}
