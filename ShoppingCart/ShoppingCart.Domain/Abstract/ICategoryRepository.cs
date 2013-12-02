using System.Linq;
using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Domain.Abstract
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }

        void SaveCategory(Category category);

        Category DeleteCategory(int categoryID);
    }
}
