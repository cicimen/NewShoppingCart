using ShoppingCart.Data.Abstract;
using System.Collections.Generic;
using ShoppingCart.Data.Entity;

namespace ShoppingCart.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IContext _context;
        private readonly ICategoryRepository _categories;

        public CategoryService(IContext context)
        {
            _context = context;
            _categories = context.Categories;
        }

        public IEnumerable<Category> GetAllRoot(Language language)
        {
            return _categories.All(language, true, false, false, true);
        }
    }
}
