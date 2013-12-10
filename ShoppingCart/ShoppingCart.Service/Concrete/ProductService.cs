using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;
using System.Collections.Generic;
using System.Linq;


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

        public Product GetBy(Language language, string productURLText)
        {
            return _products.GetBy(language, productURLText);
        }

        public IEnumerable<Product> GetByCategoryForHome(Language language, string categoryURLText,int skip, int take)
        {
            if(string.IsNullOrWhiteSpace(categoryURLText))
            {
                return _products.All(language).OrderBy(p=>p.ProductURLText).Skip(skip).Take(take);
            }
            List<string> categoryURLTexts= _context.Categories.GetBy(language, categoryURLText).Offspring.Select(o=> o.Offspring.CategoryURLText).ToList();
            categoryURLTexts.Add(categoryURLText);
            return _products.GetByCategory(language, categoryURLTexts).OrderBy(p => p.ProductURLText).Skip(skip).Take(take);
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
