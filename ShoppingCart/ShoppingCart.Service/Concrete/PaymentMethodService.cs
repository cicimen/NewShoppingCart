using System.Collections.Generic;
using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;

namespace ShoppingCart.Service
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IContext _context;
        private readonly IPaymentMethodRepository  _paymentMethods;

        public PaymentMethodService(IContext context)
        {
            _context = context;
            _paymentMethods = context.PaymentMethods;
        }

        public IEnumerable<PaymentMethod> All()
        {
            return _paymentMethods.All();
        }
    }
}
