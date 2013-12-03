using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Data.Entity
{
    [Table("ProductRelation")]
    public class ProductRelation
    {
        public int ProductID { get; set; }
        public int RelatedProductID { get; set; }
        public int DisplayOrder { get; set; }


        
        [ForeignKey("ProductID")]
        //[InverseProperty("RelatedProducts")]
        public Product Product { get; set; }


        
        [ForeignKey("RelatedProductID")]
        public Product RelatedProduct { get; set; }
    }
}
