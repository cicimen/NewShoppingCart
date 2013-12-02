using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Collections.Generic;

namespace ShoppingCart.Domain.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal OriginalPrice { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal DiscountedPrice { get; set; }
        public bool Enabled { get; set; }
        [Required(ErrorMessage = "Please specify a category")]
        public int CategoryID { get; set; }
        public string ProductURLText { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int Inventory { get; set; }
        public string ProductMetaTags { get; set; }
        public string ProductMetaDescription { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<ProductImage> ProductImages { get; set; }
        public virtual List<ProductAttribute> ProductAttributes { get; set; }
        public virtual List<ProductTranslation> ProductTranslations { get; set; }
        public virtual List<ProductComment> ProductComments{ get; set; }
        public virtual ICollection<Product> RelatedProducts { get; set; }

    }
}
