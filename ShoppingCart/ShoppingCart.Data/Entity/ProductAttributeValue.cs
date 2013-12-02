using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Data.Entity
{
    [Table("ProductAttributeValue")]
    public class ProductAttributeValue
    {
        public int ProductAttributeValueID { get; set; }
        public int ProductID { get; set; }
        public int ProductAttributeID{get;set;}



        [ForeignKey("ProductAttributeID")]
        public virtual ProductAttribute ProductAttribute { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }


        public virtual ICollection<ProductAttributeValueTranslation> ProductAttributeValueTranslations
        {
            get { return _productAttributeValueTranslation ?? (_productAttributeValueTranslation = new Collection<ProductAttributeValueTranslation>()); }
            set { _productAttributeValueTranslation = value; }
        }





         private ICollection<ProductAttributeValueTranslation> _productAttributeValueTranslation;
    }
}
