using System.Linq;
using ShoppingCart.Data.Entity;

namespace ShoppingCart.Data.Abstract
{
    public interface IPaymentMethodRepository :IRepository<PaymentMethod>
    {
    }
}
