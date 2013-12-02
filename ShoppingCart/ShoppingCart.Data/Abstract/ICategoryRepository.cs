using System.Linq;
using ShoppingCart.Data.Entity;

namespace ShoppingCart.Data.Abstract
{
    public interface ICategoryRepository :IRepository<Category>
    {
        IQueryable<Category> All(Language language, bool includeCategoryTranslations = false, bool includeAncestors = false,
            bool includeOffspring = false, bool includeChildren = false);
        Category GetBy(Language language, string categoryURLText, bool includeCategoryTranslations = false, bool includeOffspring = false,
            bool includeAncestors = false, bool includeChildren = false);

        void DeleteCategory(int id);
    }
}
