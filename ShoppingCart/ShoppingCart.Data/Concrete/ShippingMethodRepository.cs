using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace ShoppingCart.Data.Concrete
{
    public class ShippingMethodRepository : EfRepository<ShippingMethod>, IShippingMethodRepository
    {
        public ShippingMethodRepository(DbContext context, bool sharedContext)
            : base(context, sharedContext) { }

    }
}
