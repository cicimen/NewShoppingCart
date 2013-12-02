using ShoppingCart.Domain.Abstract;
using ShoppingCart.Domain.Concrete;
using ShoppingCart.Domain.Entities;
using System.Linq;

namespace ShoppingCart.Domain.Concrete
{
    public class EFCategoryRepository :ICategoryRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public IQueryable<Category> Categories
        {
            get { return context.Categories; }
        }

        public void SaveCategory(Category category)
        {
            if (category.CategoryID == 0)
            {
                context.Categories.Add(category);
            }
            else
            {
                Category dbEntry = context.Categories.Find(category.CategoryID);
                if (dbEntry != null)
                {
                    dbEntry.Ancestors = category.Ancestors;
                    dbEntry.CategoryTranslations = category.CategoryTranslations;
                    dbEntry.CategoryURLText = category.CategoryURLText;
                    dbEntry.Children = category.Children;
                    dbEntry.DateCreated = category.DateCreated;
                    dbEntry.DateModified = category.DateModified;
                    dbEntry.Children = category.Children;
                    dbEntry.Offspring = category.Offspring;
                    dbEntry.Parent = category.Parent;
                }
            }
            context.SaveChanges();
        }

        public Category DeleteCategory(int categoryID)
        {
            Category dbEntry = context.Categories.Find(categoryID);
            if (dbEntry != null)
            {
                context.Categories.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
