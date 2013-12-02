using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Data.Entity
{
    [Table("Language")]
    public class Language
    {
        public int LanguageID { get; set; }
        [MaxLength(30)]
        public string LanguageName { get; set; }
        [MaxLength(10)]
        public string LanguageCode { get; set; }
        public bool Default { get; set; }
    }
}
