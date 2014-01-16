using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;
using System.Collections.Generic;
using System.Linq;

using ShoppingCart.Service.ViewModel;

namespace ShoppingCart.Service
{
    public class ProductService :IProductService
    {
        private readonly IContext _context;
        private readonly IProductRepository _products;

        public ProductService(IContext context)
        {
            _context = context;
            _products = context.Products;
        }

        public Product GetBy(Language language, int id)
        {
            return _products.GetBy(language,id);
        }

        public ProductViewModel GetBy(Language language, string productURLText)
        {
            Product product = _products.GetBy(language, productURLText,true,true,true,true,true,true);
            if(product == null)
            {
                return null;
            }
            ProductViewModel model = new ProductViewModel
            {
                ProductID = product.ProductID,
                DiscountedPrice = product.DiscountedPrice,
                OriginalPrice = product.OriginalPrice,
                Enabled = product.Enabled,
                Inventory = product.Inventory,
                ProductDescription = product.ProductTranslations.SingleOrDefault(x => x.Language == language).ProductDescription,
                ProductLongDescription = product.ProductTranslations.SingleOrDefault(x => x.Language == language).ProductLongDescription,
                ProductName = product.ProductTranslations.SingleOrDefault(x => x.Language == language).ProductName,
                ProductDisplayOrder = product.ProductDisplayOrder,
                ProductImages = product.ProductImages.ToList(),
                ProductComments = product.ProductComments.ToList(),
                ProductURLText = product.ProductURLText,
                ProductMetaDescription = product.ProductMetaDescription,
                ProductMetaTags = product.ProductMetaTags,
                RelatedProducts = new List<ProductViewModel> { },
                ProductAttributes = new List<ProductAttributeViewModel> { },
                ProductVariants = new List<ProductVariantViewModel> { }
            };

            foreach (var relatedProduct in product.RelatedProducts)
            {
                model.RelatedProducts.Add(new ProductViewModel {
                    ProductName = relatedProduct.RelatedProduct.ProductTranslations.SingleOrDefault(x => x.Language == language).ProductName,
                    ProductID = relatedProduct.RelatedProductID,
                    ProductImages =  relatedProduct.RelatedProduct.ProductImages.ToList()
                });
            }
            foreach (var productAttribute in product.ProductAttributeValues)
            {
                model.ProductAttributes.Add(new ProductAttributeViewModel { 
                    ProductAttributeID = productAttribute.ProductAttributeID,
                    ProductAttributeValueID = productAttribute.ProductAttributeValueID,
                    ProductAttributeName = productAttribute.ProductAttribute.ProductAttributeTranslations.SingleOrDefault(x=>x.Language == language).ProductAttributeTranslationName,
                    ProductAttributeValue = productAttribute.ProductAttributeValueTranslations.SingleOrDefault(x=>x.Language == language).ProductAttributeValueName
                });
            }

            foreach (var productVariant in product.ProductVariants)
            {
                model.ProductVariants.Add(new ProductVariantViewModel
                {
                    ProductVariantID = productVariant.ProductVariantID,
                    ProductVariantName = productVariant.ProductVariantTranslations.SingleOrDefault(pv => pv.Language == language).ProductVariantName,
                    ProductVariantValues = new List<ProductVariantValueViewModel> { }
                });
                foreach (var productVariantValue in productVariant.ProductVariantValues)
                {
                    model.ProductVariants.SingleOrDefault(pv => pv.ProductVariantID == productVariant.ProductVariantID).ProductVariantValues.
                        Add(new ProductVariantValueViewModel { 
                            ProductVariantValueID = productVariantValue.ProductVariantValueID,
                            ProductVariantValueName = productVariantValue.ProductVariantValueTranslations.SingleOrDefault(pvvt=> pvvt.Language == language).ProductVariantValueName,
                            ProductVariantValueInventory = productVariantValue.Inventory
                        });
                }
            }


            return model;
        }

        public ProductsListViewModel GetProductListViewModel(Language language,string categoryURLText,int page,int pageSize)
        {
            List<ProductViewModel> productModels = new List<ProductViewModel>();
            List<Product> products;
            if (string.IsNullOrWhiteSpace(categoryURLText))
            {
                products = _products.All(language).OrderBy(p=>p.ProductDisplayOrder).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                List<string> categoryURLTexts = _context.Categories.GetBy(language, categoryURLText).Offspring.Select(o => o.Offspring.CategoryURLText).ToList();
                categoryURLTexts.Add(categoryURLText);
                products = _products.GetByCategory(language, categoryURLTexts).OrderBy(p => p.ProductDisplayOrder).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            foreach (var item in products)
            {
                productModels.Add(new ProductViewModel { 
                ProductID = item.ProductID,
                DiscountedPrice = item.DiscountedPrice,
                OriginalPrice = item.OriginalPrice,
                Enabled = item.Enabled,
                Inventory = item.Inventory,
                ProductDescription = item.ProductTranslations.SingleOrDefault(x=> x.Language == language).ProductDescription,
                ProductLongDescription = item.ProductTranslations.SingleOrDefault(x => x.Language == language).ProductLongDescription,
                ProductName = item.ProductTranslations.SingleOrDefault(x => x.Language == language).ProductName,
                ProductDisplayOrder = item.ProductDisplayOrder,
                ProductImages = item.ProductImages.ToList(),
                ProductURLText = item.ProductURLText
                });
            }


            return new ProductsListViewModel
            {
                Products = productModels,
                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = GetCountByCategoryForHome(language, categoryURLText)
                },
                CurrentCategory = categoryURLText
            };
        }


        public IEnumerable<Product> GetByCategoryForHome(Language language, string categoryURLText,int skip, int take)
        {
            if(string.IsNullOrWhiteSpace(categoryURLText))
            {
                return _products.All(language).OrderBy(p=>p.ProductDisplayOrder).Skip(skip).Take(take);
            }
            List<string> categoryURLTexts= _context.Categories.GetBy(language, categoryURLText).Offspring.Select(o=> o.Offspring.CategoryURLText).ToList();
            categoryURLTexts.Add(categoryURLText);
            return _products.GetByCategory(language, categoryURLTexts, true, false, true, false, false, false).OrderBy(p => p.ProductDisplayOrder).Skip(skip).Take(take);
        }

        public int GetCountByCategoryForHome(Language language, string categoryURLText)
        {
            if (string.IsNullOrWhiteSpace(categoryURLText))
            {
                return _products.Count(null) ;
            }
            List<string> categoryURLTexts = _context.Categories.GetBy(language, categoryURLText).Offspring.Select(o => o.Offspring.CategoryURLText).ToList();
            categoryURLTexts.Add(categoryURLText);
            return _products.Count(p=>p.Category.CategoryURLText == categoryURLText);
        }
    }
}
