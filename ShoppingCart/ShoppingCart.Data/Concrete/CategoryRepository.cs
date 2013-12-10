using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace ShoppingCart.Data.Concrete
{
    public class CategoryRepository : EfRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext context, bool sharedContext)
            : base(context, sharedContext) { }

        public IQueryable<Category> All(Language language, bool includeCategoryTranslations = false, bool includeAncestors = false, 
            bool includeOffspring = false,  bool includeChildren = false)
        {
            var query = BuildCategoryQuery(language, includeCategoryTranslations, includeAncestors,includeOffspring, includeChildren);

            return query.AsQueryable();
        }

        public Category GetBy(Language language, string categoryURLText, bool includeCategoryTranslations = false, bool includeOffspring = false,
            bool includeAncestors = false, bool includeChildren = false)
        {
            var query = BuildCategoryQuery(language, includeCategoryTranslations, includeAncestors,includeOffspring, includeChildren);

            return query.SingleOrDefault(c => c.CategoryURLText == categoryURLText);
        }

        
        public void DeleteCategory(int id)
        {
            Category category = Find(c => c.CategoryID == id);
            if (category != null)
            {
                Delete(category);
                if (!ShareContext)
                {
                    Context.SaveChanges();
                }
            }
        }

        private IQueryable<Category> BuildCategoryQuery(Language language, bool includeCategoryTranslations,bool includeAncestors,bool includeOffspring, bool includeChildren)
        {
            var query = DbSet.AsQueryable();

            if (includeCategoryTranslations)
                //query = DbSet.Include(c => c.CategoryTranslations.Select(ct => ct.Language == language));
                query = DbSet.Include(c => c.CategoryTranslations);
            if (includeAncestors)
                query = DbSet.Include(c => c.Ancestors);
            if (includeOffspring)
                query = DbSet.Include(c => c.Offspring);
            if (includeChildren)
                query = DbSet.Include(c => c.Children);
            return query;
        }


    }
}
