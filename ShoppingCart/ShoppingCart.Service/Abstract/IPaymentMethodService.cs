using ShoppingCart.Data.Entity;
using System.Collections.Generic;

namespace ShoppingCart.Service
{
    public interface IPaymentMethodService
    {
        IEnumerable<PaymentMethod> All();
    }
}
