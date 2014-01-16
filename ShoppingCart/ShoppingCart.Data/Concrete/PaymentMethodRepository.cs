using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace ShoppingCart.Data.Concrete
{
    public class PaymentMethodRepository : EfRepository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(DbContext context, bool sharedContext)
            : base(context, sharedContext) { }

    }
}
