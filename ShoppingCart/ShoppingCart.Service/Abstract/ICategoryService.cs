using ShoppingCart.Data.Entity;
using System.Collections.Generic;

namespace ShoppingCart.Service
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllRoot(Language language);
    }
}
