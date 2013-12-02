using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Entities
{
    [Table("ProductAttribute")]
    public class ProductAttribute
    {
        public int ProductAttributeID { get; set; }
        public int ProductID { get; set; }
        public bool Enabled { get; set; }
        public virtual Product Product { get; set; }
        public virtual List<ProductAttributeTranslation> ProductAttributeTranslations { get; set; }
        public virtual List<ProductAttributeValue> ProductAttributeValues { get; set; }
    }
}
