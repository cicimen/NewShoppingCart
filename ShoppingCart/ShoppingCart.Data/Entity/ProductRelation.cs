using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart.Data.Entity
{
    [Table("ProductRelation")]
    public class ProductRelation
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProductRelationID { get; set; }
        public int ProductID { get; set; }
        public int RelatedProductID { get; set; }

        //[ForeignKey("ProductID")]
        //[InverseProperty("RelatedProducts")]
        public virtual Product Product { get; set; }

        //[ForeignKey("RelatedProductID")]
        //[Column("RelatedProductID")]
        public virtual Product RelatedProduct { get; set; }
        
        
       

        public int DisplayOrder { get; set; }
        
        
    }
}
