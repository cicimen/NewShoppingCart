using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace ShoppingCart.Data.Concrete
{
    public class LanguageRepository :EfRepository<Language>,ILanguageRepository 
    {
        public LanguageRepository(DbContext context, bool sharedContext)
            : base(context, sharedContext) { }
    }
}
