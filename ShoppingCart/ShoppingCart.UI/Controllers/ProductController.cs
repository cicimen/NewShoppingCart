using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;
using ShoppingCart.UI.Models;

using ShoppingCart.Service.ViewModel;
using ShoppingCart.UI.Helpers;

namespace ShoppingCart.UI.Controllers
{
    public class ProductController : ShoppingCartControllerBase
    {
        //private IProductRepository productRepository;

        //public ProductController(IProductRepository productRepository)
        //{
        //    this.productRepository = productRepository;
        //}

        public ActionResult Index(string productURLText)
        {
            if(string.IsNullOrWhiteSpace(productURLText))
            {
                return RedirectToAction("Index", "Home", new{page =1});
            }

            var product = Products.GetBy(Languages.GetLanguage(), productURLText);
            if (product == null)
            {
                return RedirectToAction("Index", "Home", new { page = 1 });
            }
            int productInventory = 0;
            ProductVariantViewModel firstProductVariant = product.ProductVariants.FirstOrDefault();
            if (firstProductVariant == null)
            {
                productInventory = product.Inventory;
            }
            else
            {
                List<ProductVariantValueViewModel> firstProductVariantValues = firstProductVariant.ProductVariantValues.ToList();

                foreach (ProductVariantValueViewModel productVariantValue in firstProductVariantValues)
                {
                    productInventory += productVariantValue.ProductVariantValueInventory;
                }
            }
            ViewBag.ProductInventory = productInventory>10 ? 10 : productInventory ;
            return View(product);

            
        }
	}
}