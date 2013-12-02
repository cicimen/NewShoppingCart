using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Service
{
    public class CartService : ICartService
    {
        private readonly IContext _context;
        private readonly ICartRepository _carts;
        private readonly string _cartSessionKey = "CartID";
        private HttpContext _contextBase;
        private string _shoppingCartID;

        public CartService(IContext context,HttpContextBase contextBase=null)
        {
            _context = context;
            _carts = context.Carts;
            _shoppingCartID = GetCartId(contextBase);
            _contextBase = contextBase.ApplicationInstance.Context ?? HttpContext.Current;
        }

        public IQueryable<Cart> Carts()
        {
           return _carts.All(); 
        }

        public void AddToCart(Product product)
        {
            // Get the matching cart and album instances
            var cartItem = _carts.Find(c => c.CartId == _shoppingCartID && c.ProductId == product.ProductID);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    ProductId = product.ProductID,
                    CartId = _shoppingCartID,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                _carts.Create(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count++;
            }
            // Save changes
            _context.SaveChanges();
        }

        public void AddToCart(Product product, int itemCount, string variationXml)
        {
            // Get the matching cart and album instances
            var cartItem = _carts.Find(c => c.CartId == _shoppingCartID && c.ProductId == product.ProductID && c.VariationXml == variationXml);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    ProductId = product.ProductID,
                    CartId = _shoppingCartID,
                    Count = itemCount,
                    DateCreated = DateTime.Now,
                    VariationXml = variationXml
                };
                _carts.Create(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count += itemCount;
            }
            // Save changes
            _context.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem =_carts.Find(cart => cart.CartId == _shoppingCartID && cart.RecordId == id);
            int itemCount = 0;
            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _carts.Delete(cartItem);
                }
                // Save changes
                _context.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = _carts.FindAll(cart => cart.CartId == _shoppingCartID);
            foreach (var cartItem in cartItems)
            {
                _carts.Delete(cartItem);
            }
            // Save changes
            _context.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return _carts.FindAll(cart => cart.CartId == _shoppingCartID).ToList();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in _carts.All()
                          where cartItems.CartId == _shoppingCartID
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in _carts.All()
                              where cartItems.CartId == _shoppingCartID
                              select (int?)cartItems.Count * cartItems.Product.DiscountedPrice).Sum();
            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            var cartItems = GetCartItems();
            // Iterate over the items in the cart, adding the order details for each
            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    ProductID = item.ProductId,
                    OrderID = order.OrderId,
                    UnitPrice = item.Product.DiscountedPrice,
                    Quantity = item.Count,
                    VariationXml = item.VariationXml
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Product.DiscountedPrice);
                _context.OrderItems.Create(orderItem);
            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;
            // Save the order
            _context.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.OrderId;
        }

        public void MigrateCart(string userName,HttpContextBase contextBase)
        {
            var shoppingCart = _carts.FindAll(c => c.CartId == _shoppingCartID);
            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            _context.SaveChanges();
            contextBase.Session[_cartSessionKey] = contextBase.User.Identity.Name; 
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[_cartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[_cartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[_cartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[_cartSessionKey].ToString();
        }

    }
}
