using ShoppingCart.Data.Entity;
using System.Collections.Generic;

namespace ShoppingCart.Service.ViewModel
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public bool Enabled { get; set; }
        public string ProductURLText { get; set; }
        public int Inventory { get; set; }
        public string ProductMetaTags { get; set; }
        public string ProductMetaDescription { get; set; }
        public int ProductDisplayOrder { get; set; }
        public List<ProductAttributeViewModel> ProductAttributes { get; set; }
        public List<ProductVariantViewModel> ProductVariants { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductComment> ProductComments { get; set; }
        public List<ProductViewModel> RelatedProducts { get; set; }
    }
}
