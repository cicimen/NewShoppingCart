using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Data.Entity
{
    [Table("CategoryTranslation")]
    public class CategoryTranslation
    {
        public int CategoryTranslationID { get; set; }

        [MaxLength(100)]
        public string CategoryName { get; set; }
        [MaxLength(500)]
        public string CategoryDescription { get; set; }
        public int LanguageID { get; set; }
        public int CategoryID { get; set; }



        [ForeignKey("LanguageID")]
        public virtual Language Language { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
    }
}
