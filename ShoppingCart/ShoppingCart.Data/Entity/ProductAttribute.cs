using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;


namespace ShoppingCart.Data.Entity
{
    [Table("ProductAttribute")]
    public class ProductAttribute
    {
        public int ProductAttributeID { get; set; }

        public int ProductAttributeDisplayOrder { get; set; }

        private ICollection<ProductAttributeTranslation> _productAttributeTranslations;

        public virtual ICollection<ProductAttributeTranslation> ProductAttributeTranslations
        {
            get { return _productAttributeTranslations ?? (_productAttributeTranslations = new Collection<ProductAttributeTranslation>()); }
            set { _productAttributeTranslations = value; }
        }
    }
}
