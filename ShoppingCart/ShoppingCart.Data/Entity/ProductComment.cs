using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Data.Entity
{
    [Table("ProductComment")]
    public class ProductComment
    {
        public int ProductCommentID { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Approved { get; set; }
        public string ProductCommentTitle { get; set; }
        public string ProductCommentText { get; set; }


        
        public string ApplicationUserId { get; set; }
        public int ProductID { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Product Product { get; set; }

    }
}
