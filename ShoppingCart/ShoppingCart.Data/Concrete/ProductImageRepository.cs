using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace ShoppingCart.Data.Concrete
{
    public class ProductImageRepository : EfRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(DbContext context, bool sharedContext)
            : base(context, sharedContext) { }
    }
}
