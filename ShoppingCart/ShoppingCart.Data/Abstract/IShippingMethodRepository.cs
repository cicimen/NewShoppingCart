using System.Linq;
using ShoppingCart.Data.Entity;

namespace ShoppingCart.Data.Abstract
{
    public interface IShippingMethodRepository : IRepository<ShippingMethod>
    {
    }
}
