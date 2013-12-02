using ShoppingCart.Data.Entity;
using System.Collections.Generic;

namespace ShoppingCart.Service
{
    public interface IProductService
    {
        Product GetBy(Language language, int id);
        Product GetBy(Language language, string productURLText);
        IEnumerable<Product> GetByCategoryForHome(Language language, string categoryURLText, int skip, int take);
        int GetCountByCategoryForHome(Language language, string categoryURLText);
    }
}
