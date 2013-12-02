using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Entities
{
    [Table("Language")]
    public class Language
    {
        public int LanguageID { get; set; }

        public string LanguageName { get; set; }

        public string LanguageCode { get; set; }
    }
}
