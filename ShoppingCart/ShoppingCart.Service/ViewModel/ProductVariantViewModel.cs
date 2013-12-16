using System.Collections.Generic;

namespace ShoppingCart.Service.ViewModel
{
    public class ProductVariantViewModel
    {
        public int ProductVariantID { get; set; }
        public string ProductVariantName { get; set; }
        public List<ProductVariantValueViewModel> ProductVariantValues { get; set; }
    }
}
