using ShoppingCart.Domain.Entities;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Domain.Abstract
{
    public interface ICartRepository
    {
        ICartRepository GetCart(HttpContextBase context);

        ICartRepository GetCart(Controller controller);

        void AddToCart(Product product);

        void AddToCart(Product product, int itemCount);

        //public static ICartRepository GetCart(HttpContextBase context);

        int RemoveFromCart(int id);

        void EmptyCart();

        List<Cart> GetCartItems();

        int GetCount();

        decimal GetTotal();

        int CreateOrder(Order order);

        string GetCartId(HttpContextBase context);

        void MigrateCart(string userName);
    }
}
