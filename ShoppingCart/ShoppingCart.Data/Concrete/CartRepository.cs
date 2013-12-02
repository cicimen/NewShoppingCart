using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;
using System.Data.Entity;


namespace ShoppingCart.Data.Concrete
{
    public class CartRepository : EfRepository<Cart>, ICartRepository
    {
        public CartRepository(DbContext context, bool sharedContext)
            : base(context, sharedContext) { }
    }
}
