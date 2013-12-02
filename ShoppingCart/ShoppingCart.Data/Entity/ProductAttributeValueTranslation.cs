using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ShoppingCart.Data.Entity
{
    [Table("ProductAttributeValueTranslation")]
    public class ProductAttributeValueTranslation
    {
        public int ProductAttributeValueTranslationID { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProductAttributeValueName { get; set; }



        public int LanguageID { get; set; }
        public int ProductAttributeValueID { get; set; }

        [ForeignKey("LanguageID")]
        public virtual Language Language { get; set; }
        [ForeignKey("ProductAttributeValueID")]
        public virtual ProductAttributeValue ProductAttributeValue { get; set; }
    }
}
