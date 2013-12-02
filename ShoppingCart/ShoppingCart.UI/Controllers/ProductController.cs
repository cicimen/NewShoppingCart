using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;
using ShoppingCart.UI.Models;

using ShoppingCart.UI.Helpers;

namespace ShoppingCart.UI.Controllers
{
    public class ProductController : ShoppingCartControllerBase
    {
        private IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ActionResult Index(string productURLText)
        {
            int productInventory = 0;
            if(string.IsNullOrWhiteSpace(productURLText))
            {
                return RedirectToAction("Index", "Home", new{page =1});
            }

            var product = Products.GetBy(Languages.GetLanguage(), productURLText);
            if (product == null)
            {
                return RedirectToAction("Index", "Home", new { page = 1 });
            }
            ProductVariant firstProductVariant = product.ProductVariants.FirstOrDefault();
            if (firstProductVariant == null)
            {
                productInventory = product.Inventory;
            }
            else
            {
                List<ProductVariantValue> firstProductVariantValues = firstProductVariant.ProductVariantValues.ToList();
                List<ProductVariantValueTranslation> firstProductVariantValueTranslations = new List<ProductVariantValueTranslation>();

                foreach (ProductVariantValue productVariantValue in firstProductVariantValues)
                {
                    firstProductVariantValueTranslations.Add(productVariantValue.ProductVariantValueTranslations
                        .Where(x => x.Language.LanguageCode == CodeHelpers.GetLanguageCode()).First());
                    productInventory += productVariantValue.Inventory;
                }
                ViewBag.ProductVariantValueID = new SelectList(firstProductVariantValueTranslations, "ProductVariantValueID", "ProductVariantValueName");
            }
            ViewBag.ProductInventory = productInventory>10 ? 10 : productInventory ;
            return View(product);

            
        }
	}
}