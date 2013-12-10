using System.Collections.Generic;

using ShoppingCart.Data.Entity;

namespace ShoppingCart.UI.Models
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}