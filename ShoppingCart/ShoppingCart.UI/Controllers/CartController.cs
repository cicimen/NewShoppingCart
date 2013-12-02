using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;
using ShoppingCart.Data.Concrete;
using ShoppingCart.UI.Models;
using ShoppingCart.UI.Helpers;

namespace ShoppingCart.UI.Controllers
{
    public class CartController : ShoppingCartControllerBase
    {
        //EFCartRepository cartRepository = new EFCartRepository();
        
        //private IProductRepository productRepository;

        //public CartController(IProductRepository productRepository)
        //{
        //    this.productRepository = productRepository;
        //}
        
        //
        // GET: /Cart/
        public ActionResult Index()
        {
            //var cart = Carts.ge EFCartRepository.GetCart(this.HttpContext);
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = Carts.GetCartItems(),
                CartTotal = Carts.GetTotal()
            };
            return View(viewModel);
        }

        //
        // GET: /Store/AddToCart/5
        //public ActionResult AddToCart(int id)
        //{
        //    // Retrieve the album from the database
        //    var product = productRepository.Products.Single(x => x.ProductID == id);
        //    // Add it to the shopping cart
        //    var cart = EFCartRepository.GetCart(this.HttpContext);
        //    cart.AddToCart(product);
        //    // Go back to the main store page for more shopping
        //    return RedirectToAction("Index");
        //}


        [HttpPost]
        public ActionResult AddToCart(int productID, int count, int productAttributeValueID = 0)
        {
            // Retrieve the album from the database
            var product = Products.GetBy(Languages.GetLanguage(),productID);
            // Add it to the shopping cart
            Carts.AddToCart(product, count, CodeHelpers.CreateXmlForAttribute(productAttributeValueID));
            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        
        public ActionResult RemoveFromCart(int id)
        {
            string languageCode = Languages.GetLanguage().LanguageCode;
            // Get the name of the album to display confirmation
            string productName = Carts.Carts().SingleOrDefault(x => x.RecordId == id).Product.ProductTranslations.
                FirstOrDefault(x=> x.Language.LanguageCode == languageCode).ProductName;
            // Remove from cart
            int itemCount = Carts.RemoveFromCart(id);
            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(productName) + languageCode == "en" ? " has been removed from your shopping cart." : "sepetten çıkarıldı.",
                CartTotal = Carts.GetTotal(),
                CartCount = Carts.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            ViewData["CartCount"] = Carts.GetCount();
            return PartialView("CartSummary");
        }

    }
}