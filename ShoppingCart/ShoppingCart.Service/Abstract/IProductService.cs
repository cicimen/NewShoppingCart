using ShoppingCart.Data.Entity;
using System.Collections.Generic;
using ShoppingCart.Service.ViewModel;

namespace ShoppingCart.Service
{
    public interface IProductService
    {
        Product GetBy(Language language, int id);
        ProductViewModel GetBy(Language language, string productURLText);
        IEnumerable<Product> GetByCategoryForHome(Language language, string categoryURLText, int skip, int take);
        ProductsListViewModel GetProductListViewModel(Language language, string categoryURLText, int page, int pageSize);      
        int GetCountByCategoryForHome(Language language, string categoryURLText);
    }
}
