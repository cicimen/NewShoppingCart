using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ShoppingCart.Domain.Concrete;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Domain.Entities
{
    [Table("ProductComment")]
    public class ProductComment
    {
        public int ProductCommentID { get; set; }
        public int ProductID { get; set; }
        public bool Approved { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Product Product { get; set; }

    }
}
