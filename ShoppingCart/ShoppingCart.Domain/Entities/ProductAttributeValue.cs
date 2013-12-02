using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Entities
{
    [Table("ProductAttributeValue")]
    public class ProductAttributeValue
    {
        public int ProductAttributeValueID { get; set; }
        public int ProductAttributeID { get; set; }
        public bool Enabled { get; set; }
        public int Inventory { get; set; }
        public virtual ProductAttribute ProductAttribute { get; set; }
        public virtual List<ProductAttributeValueTranslation> ProductAttributeValueTranslations { get; set; }
    }
}
