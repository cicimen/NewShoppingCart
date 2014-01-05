using System.Collections.Generic;
using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;


namespace ShoppingCart.Service
{
    public class ShippingMethodService : IShippingMethodService
    {
        private readonly IContext _context;
        private readonly IShippingMethodRepository _shippingMethods;

        public ShippingMethodService(IContext context)
        {
            _context = context;
            _shippingMethods = context.ShippingMethods;
        }

        public IEnumerable<ShippingMethod> All()
        {
            return _shippingMethods.All();
        }
    }
}
