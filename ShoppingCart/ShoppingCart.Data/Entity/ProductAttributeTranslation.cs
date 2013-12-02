using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ShoppingCart.Data.Entity
{
    [Table("ProductAttributeTranslation")]
    public class ProductAttributeTranslation
    {
        public int ProductAttributeTranslationID { get; set; }

        [Required]
        [MaxLength(500)]
        public string ProductAttributeTranslationName { get; set; }



        public int LanguageID { get; set; }
        public int ProductAttributeID { get; set; }


        [ForeignKey("LanguageID")]
        public virtual Language Language { get; set; }
        [ForeignKey("ProductAttributeID")]
        public virtual ProductAttribute ProductAttribute { get; set; }
    }
}
