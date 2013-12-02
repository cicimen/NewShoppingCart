using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ShoppingCart.Data.Entity
{
    [Table("ProductVariantValueTranslation")]
    public class ProductVariantValueTranslation
    {
        public int ProductVariantValueTranslationID { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProductVariantValueName { get; set; }



        public int LanguageID { get; set; }
        public int ProductVariantValueID { get; set; }

        [ForeignKey("LanguageID")]
        public virtual Language Language { get; set; }
        [ForeignKey("ProductVariantValueID")]
        public virtual ProductVariantValue ProductVariantValue { get; set; }
    }
}
