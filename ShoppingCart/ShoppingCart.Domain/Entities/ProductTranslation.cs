using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Domain.Entities
{
    [Table("ProductTranslation")]
    public class ProductTranslation
    {
        public int ProductTranslationID { get; set; }

        public int LanguageID { get; set; }

        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public virtual Language Language { get; set; }

        public virtual Product Product { get; set; }
    }
}
