using System.Linq;
using ShoppingCart.Data.Entity;
using System.Collections.Generic;

namespace ShoppingCart.Data.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> All(Language language, bool includeProductImages = false, bool includeProductVariants = false,
            bool includeProductTranslations = false, bool includeProductComments = false, bool includeRelatedProducts = false, bool includeProductAttributeValues = false);

        IQueryable<Product> GetByCategory(Language language, List<string> categoryURLTexts, bool includeProductImages = false, bool includeProductVariants = false,
            bool includeProductTranslations = false, bool includeProductComments = false, bool includeRelatedProducts = false, bool includeProductAttributeValues = false);

        Product GetBy(Language language, int id, bool includeProductImages = false, bool includeProductVariants = false,
            bool includeProductTranslations = false, bool includeProductComments = false, bool includeRelatedProducts = false, bool includeProductAttributeValues = false);

        Product GetBy(Language language, string productURLText, bool includeProductImages = false, bool includeProductVariants = false,
            bool includeProductTranslations = false, bool includeProductComments = false, bool includeRelatedProducts = false, bool includeProductAttributeValues = false);

        void DeleteProduct(int id);

        void AddRelatedProduct(Product product, Product relatedProduct, int displayOrder);



    }
}
