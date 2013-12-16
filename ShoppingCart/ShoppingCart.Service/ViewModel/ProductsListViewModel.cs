using System.Collections.Generic;
using ShoppingCart.Data.Entity;

namespace ShoppingCart.Service.ViewModel
{
    public class ProductsListViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}