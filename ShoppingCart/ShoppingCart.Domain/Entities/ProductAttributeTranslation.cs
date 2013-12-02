using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Entities
{
    [Table("ProductAttributeTranslation")]
    public class ProductAttributeTranslation
    {
        public int ProductAttributeTranslationID { get; set; }

        public int LanguageID { get; set; }

        public int ProductAttributeID { get; set; }

        [Required(ErrorMessage = "Please enter a product attribute name")]
        public string ProductAttributeName { get; set; }

        public string ProductAttributeDescription { get; set; }

        public virtual Language Language { get; set; }

        public virtual ProductAttribute ProductAttribute { get; set; }
    }
}
