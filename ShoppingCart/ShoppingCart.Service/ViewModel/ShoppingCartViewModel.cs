using System.Collections.Generic;

using ShoppingCart.Data.Entity;

namespace ShoppingCart.Service.ViewModel
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
        public int CartCount { get; set; }
    }
}