using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ShoppingCart.Domain.Abstract;
using ShoppingCart.Domain.Entities;


namespace ShoppingCart.Domain.Concrete
{
    public class EFCartRepository //: ICartRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public IQueryable<Cart> Carts
        {
            get { return context.Carts; }
        }
        string ShoppingCartID { get; set; }

        public const string CartSessionKey = "CartID";

        ////Alttaki iki metod interface'de staik metod kullanılamadığı için yazıldı.
        //EFCartRepository ICartRepository.GetCart(HttpContextBase context)
        //{
        //    return EFCartRepository.GetCart(context);
        //}

        //EFCartRepository ICartRepository.GetCart(Controller controller)
        //{
        //    return EFCartRepository.GetCart(controller);
        //}

        public static EFCartRepository GetCart(HttpContextBase context)
        {
            var cart = new EFCartRepository();
            cart.ShoppingCartID = cart.GetCartId(context);
            return cart;
        }

        // Helper method to simplify shopping cart calls
        public static EFCartRepository GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Product product)
        {
            // Get the matching cart and album instances
            var cartItem = context.Carts.SingleOrDefault(c => c.CartId == ShoppingCartID && c.ProductId == product.ProductID);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    ProductId = product.ProductID,
                    CartId = ShoppingCartID,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                context.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count++;
            }
            // Save changes
            context.SaveChanges();
        }

        public void AddToCart(Product product, int itemCount,string attributesXml)
        {
            // Get the matching cart and album instances
            var cartItem = context.Carts.SingleOrDefault(c => c.CartId == ShoppingCartID && c.ProductId == product.ProductID &&c.AttributesXml == attributesXml);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    ProductId = product.ProductID,
                    CartId = ShoppingCartID,
                    Count = itemCount,
                    DateCreated = DateTime.Now,
                    AttributesXml =attributesXml
                };
                context.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count +=itemCount;
            }
            // Save changes
            context.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = context.Carts.Single(cart => cart.CartId == ShoppingCartID && cart.RecordId == id);
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
                    context.Carts.Remove(cartItem);
                }
                // Save changes
                context.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = context.Carts.Where(cart => cart.CartId == ShoppingCartID);
            foreach (var cartItem in cartItems)
            {
                context.Carts.Remove(cartItem);
            }
            // Save changes
            context.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return context.Carts.Where(cart => cart.CartId == ShoppingCartID).ToList();
        }

        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in context.Carts
                          where cartItems.CartId == ShoppingCartID
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in context.Carts
                              where cartItems.CartId == ShoppingCartID
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
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Product.DiscountedPrice,
                    Quantity = item.Count,
                    AttributesXml =item.AttributesXml
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Product.DiscountedPrice);
                context.OrderDetails.Add(orderDetail);
            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;
            // Save the order
            context.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.OrderId;
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = context.Carts.Where(c => c.CartId == ShoppingCartID);
            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            context.SaveChanges();
        }
    }
}
