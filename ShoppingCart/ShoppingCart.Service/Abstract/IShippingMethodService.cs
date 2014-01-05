using ShoppingCart.Data.Entity;
using System.Collections.Generic;

namespace ShoppingCart.Service
{
    public interface IShippingMethodService
    {
        IEnumerable<ShippingMethod> All();
    }
}
