using ShoppingCart.Data.Entity;
using System.Collections.Generic;

namespace ShoppingCart.Service
{
    public interface ICityService
    {
        IEnumerable<City> All();
    }
}
