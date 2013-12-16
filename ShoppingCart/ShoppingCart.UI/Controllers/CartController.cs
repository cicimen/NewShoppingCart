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

using ShoppingCart.Service.ViewModel;

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
                CartItems = Carts.GetCart(this.HttpContext).GetCartItems(),
                CartTotal = Carts.GetTotal(),
                CartCount = Carts.GetCount()
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
        public ActionResult AddToCart(int productID, int count, int productVariantValueID = 0)
        {
            // Retrieve the album from the database
            var product = Products.GetBy(Languages.GetLanguage(),productID);
            // Add it to the shopping cart
            Carts.GetCart(this.HttpContext).AddToCart(product, count, CodeHelpers.CreateXmlForAttribute(productVariantValueID));
            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        
        public JsonResult RemoveFromCart(int id)
        {
            string languageCode = Languages.GetLanguage().LanguageCode;
            // Get the name of the album to display confirmation
            string productName = Carts.GetCart(this.HttpContext).Carts().SingleOrDefault(x => x.RecordId == id).Product.ProductTranslations.
                FirstOrDefault(x=> x.Language.LanguageCode == languageCode).ProductName;
            // Remove from cart
            int itemCount = Carts.GetCart(this.HttpContext).RemoveFromCart(id);
            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(productName) + languageCode == "en" ? " has been removed from your shopping cart." : "sepetten çıkarıldı.",
                CartTotal = Carts.GetCart(this.HttpContext).GetTotal(),
                CartCount = Carts.GetCart(this.HttpContext).GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        [HttpPost]
        public JsonResult UpdateCart(List<UpdateCartViewModel> updateCartViewModels)
        {
            foreach (UpdateCartViewModel model in updateCartViewModels)
            {
                var cartItem = Carts.GetCart(this.HttpContext).Carts().SingleOrDefault(x => x.RecordId == model.RecordID);
                if(cartItem!= null)
                {
                    int itemCount = Carts.GetCart(this.HttpContext).UpdateCartItem(model.RecordID, model.Count);
                }
            }

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = Carts.GetCart(this.HttpContext).GetCartItems(),
                CartTotal = Carts.GetTotal(),
                CartCount =Carts.GetCount()
            };
            var result = new { Success = "True"};
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            ViewData["CartCount"] = Carts.GetCart(this.HttpContext).GetCount();
            return PartialView("CartSummary");
        }

    }
}