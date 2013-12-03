using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace ShoppingCart.Data.Entity
{
    [Table("ProductVariantValue")]
    public class ProductVariantValue
    {
        public int ProductVariantValueID { get; set; }
        public bool Enabled { get; set; }
        public int Inventory { get; set; }

        public int ProductVariantID { get; set; }


        [ForeignKey("ProductVariantID")]
        public virtual ProductVariant ProductVariant { get; set; }
        private ICollection<ProductVariantValueTranslation> _productVariantValueTranslations;
        public virtual ICollection<ProductVariantValueTranslation> ProductVariantValueTranslations
        {
            get { return _productVariantValueTranslations ?? (_productVariantValueTranslations = new Collection<ProductVariantValueTranslation>()); }
            set { _productVariantValueTranslations = value; }
        }
    }
}
