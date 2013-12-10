using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Concrete;
using ShoppingCart.Service;
using Microsoft.AspNet.Identity;
using ShoppingCart.Data.Entity;

namespace ShoppingCart.UI.Controllers
{
    public class ShoppingCartControllerBase : Controller
    {
        protected IContext DataContext;
        public IAddressService Addresses { get; private set; }
        public ICategoryService Categories { get; private set; }
        public IProductService Products{ get; private set; }
        public IProductImageService ProductImages { get; private set; }
        public ICartService Carts { get; private set; }
        public ICityService Cities { get; private set; }
        public ILanguageService Languages { get; private set; }
        public UserManager<ApplicationUser> UserManager { get; private set; }

        public ShoppingCartControllerBase()
        {
            DataContext = new Context();
            Addresses = new AddressService(DataContext);
            Categories = new CategoryService(DataContext);
            Products = new ProductService(DataContext);
            ProductImages = new ProductImageService(DataContext);
            Carts = new CartService(DataContext);
            Cities = new CityService(DataContext);
            Languages = new LanguageService(DataContext);
            UserManager = DataContext.UserManager;
            UserManager.UserValidator = new UserValidator<ApplicationUser>(UserManager)
            {
                AllowOnlyAlphanumericUserNames= false                
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (DataContext != null)
            {
                DataContext.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult GoToReferrer()
        {
            if (Request.UrlReferrer != null)
            {
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            }

            return RedirectToAction("Index", "Home");
        }
	}
}