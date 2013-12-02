using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Entities
{
    [Table("ProductAttributeValueTranslation")]
    public class ProductAttributeValueTranslation
    {
        public int ProductAttributeValueTranslationID { get; set; }

        public int LanguageID { get; set; }

        public int ProductAttributeValueID { get; set; }

        [Required(ErrorMessage = "Please enter a product attribute name")]
        public string ProductAttributeValueName { get; set; }

        public virtual Language Language { get; set; }

        public virtual ProductAttributeValue ProductAttributeValues { get; set; }
    }
}
