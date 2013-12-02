using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Entities
{
    [Table("ProductImage")]
    public class ProductImage
    {
        public int ProductImageID { get; set; }

        public int ProductID { get; set; }

        public string ProductImagePath { get; set; }

        public string ProductImageMimeType { get; set; }

        public short DisplayOrder { get; set; }

        public virtual Product Product { get; set; }
    }
}
