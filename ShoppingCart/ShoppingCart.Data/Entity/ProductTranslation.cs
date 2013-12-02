using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Data.Entity
{
    [Table("ProductTranslation")]
    public class ProductTranslation
    {
        public int ProductTranslationID { get; set; }
        [Required]
        [MaxLength(1000)]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }



        public int LanguageID { get; set; }
        public int ProductID { get; set; }



        [ForeignKey("LanguageID")]
        public virtual Language Language { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
