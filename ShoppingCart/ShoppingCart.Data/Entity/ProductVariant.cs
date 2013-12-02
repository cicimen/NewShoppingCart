using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Data.Entity
{
    [Table("ProductVariant")]
    public class ProductVariant
    {
        public int ProductVariantID { get; set; }
        public bool Enabled { get; set; }
        public int ProductVariantDisplayOrder { get; set; }
        public int ProductID { get; set; }



        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        private ICollection<ProductVariantTranslation> _productVariantTranslations;
        private ICollection<ProductVariantValue> _productVariantValues;

        public virtual ICollection<ProductVariantTranslation> ProductVariantTranslations
        {
            get { return _productVariantTranslations ?? (_productVariantTranslations = new Collection<ProductVariantTranslation>()); }
            set { _productVariantTranslations = value; }
        }
        public virtual ICollection<ProductVariantValue> ProductVariantValues
        {
            get { return _productVariantValues ?? (_productVariantValues = new Collection<ProductVariantValue>()); }
            set { _productVariantValues = value; }
        }
    }
}
