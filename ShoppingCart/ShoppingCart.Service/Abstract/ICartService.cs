using ShoppingCart.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Service
{
    public interface ICartService
    {
        IQueryable<Cart> Carts();
        CartService GetCart(HttpContextBase context);
        void AddToCart(Product product);
        void AddToCart(Product product, int itemCount, string variationXml);
        int RemoveFromCart(int id);
        void EmptyCart();
        List<Cart> GetCartItems();
        int GetCount();
        decimal GetTotal();
        int CreateOrder(Order order);
        void MigrateCart(string userName, HttpContextBase contextBase);
        string GetCartId(HttpContextBase context);
    }
}
