using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Domain.Entities
{
    [Table("CategoryTranslation")]
    public class CategoryTranslation
    {
        public int CategoryTranslationID { get; set; }

        public int LanguageID { get; set; }

        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Please enter a category name")]
        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }

        public virtual Language Language { get; set; }

        public virtual Category Category { get; set; }
    }
}
