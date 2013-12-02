using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Data.Entity
{
    [Table("ProductVariantTranslation")]
    public class ProductVariantTranslation
    {
        public int ProductVariantTranslationID { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductVariantName { get; set; }

        [MaxLength(500)]
        public string ProductVariantDescription { get; set; }


        public int LanguageID { get; set; }
        public int ProductVariantID { get; set; }


        [ForeignKey("LanguageID")]
        public virtual Language Language { get; set; }
        [ForeignKey("ProductVariantID")]
        public virtual ProductVariant ProductVariant { get; set; }
    }
}
