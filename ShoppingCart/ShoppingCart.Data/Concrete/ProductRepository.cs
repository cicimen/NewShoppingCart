using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ShoppingCart.Data.Concrete
{
    public class ProductRepository : EfRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context, bool sharedContext)
            : base(context, sharedContext) { }

        public IQueryable<Product> All(Language language, bool includeProductImages = false, bool includeProductVariants = false,
            bool includeProductTranslations = false, bool includeProductComments = false, bool includeRelatedProducts = false, bool includeProductAttributeValues = false)
        {
            var query = BuildProductQuery(language,includeProductImages, includeProductVariants, includeProductTranslations,
                includeProductComments, includeRelatedProducts, includeProductAttributeValues);

            return query.AsQueryable();
        }


        public IQueryable<Product> GetByCategory(Language language,List<string> categoryURLTexts, bool includeProductImages = false, bool includeProductVariants = false,
            bool includeProductTranslations = false, bool includeProductComments = false, bool includeRelatedProducts = false, bool includeProductAttributeValues = false)
        {
            var query = BuildProductQuery(language, includeProductImages, includeProductVariants, includeProductTranslations,
                includeProductComments, includeRelatedProducts, includeProductAttributeValues).Where(p=> categoryURLTexts.Contains(p.Category.CategoryURLText));

            return query.AsQueryable();
        }


        public Product GetBy(Language language, int id, bool includeProductImages = false, bool includeProductVariants = false,
            bool includeProductTranslations = false, bool includeProductComments = false, bool includeRelatedProducts = false,bool includeProductAttributeValues=false)
        {
            var query = BuildProductQuery(language,includeProductImages, includeProductVariants, includeProductTranslations,
                includeProductComments, includeRelatedProducts, includeProductAttributeValues);

            return query.SingleOrDefault(p => p.ProductID == id);
        }
        public Product GetBy(Language language, string productURLText, bool includeProductImages = false, bool includeProductVariants = false,
            bool includeProductTranslations = false, bool includeProductComments = false, bool includeRelatedProducts = false, bool includeProductAttributeValues=false)
        {
            var query = BuildProductQuery(language,includeProductImages, includeProductVariants, includeProductTranslations,
                includeProductComments, includeRelatedProducts, includeProductAttributeValues);

            return query.SingleOrDefault(p => p.ProductURLText == productURLText);
        }
        public void DeleteProduct(int id)
        {
            Product product = Find(p => p.ProductID== id);
            if (product != null)
            {
                Delete(product);
                if (!ShareContext)
                {
                    Context.SaveChanges();
                }
            }
        }
        public void AddRelatedProduct(Product product, Product relatedProduct,int displayOrder)
        {
            //TODO: DbSet.Attach ne işe yarıyor?
            //DbSet.Attach(relatedProduct);
            product.RelatedProducts.Add(new ProductRelation { Product = product, RelatedProduct =relatedProduct,DisplayOrder = displayOrder});
            if (!ShareContext)
            {
                Context.SaveChanges();
            }
        }
        private IQueryable<Product> BuildProductQuery(Language language, bool includeProductImages, bool includeProductVariants,
            bool includeProductTranslations, bool includeProductComments, bool includeRelatedProducts,bool includeProductAttributeValues)
        {
            var query = DbSet.AsQueryable();

            if (includeProductImages)
                query = DbSet.Include(p => p.ProductImages);

            if (includeProductVariants)
                query = DbSet.Include(p => p.ProductVariants.Select(pv => pv.ProductVariantTranslations.Select(x => x.Language == language)))
                    .Include(p => p.ProductVariants.Select(pv => pv.ProductVariantValues.Select(pvv => pvv.ProductVariantValueTranslations == language)));

            if (includeProductTranslations)
                //query = DbSet.Include(p => p.ProductTranslations.Select(pt=> pt.Language == language));
                //query = DbSet.Include(p => p.ProductTranslations.Where(pt => pt.Language == language));
                query = DbSet.Include(p => p.ProductTranslations);//.Select(pt => pt.Language == language));

            if (includeProductComments)
                query = DbSet.Include(p => p.ProductComments);

            if (includeRelatedProducts)
                query = DbSet.Include(p => p.RelatedProducts);

            if(includeProductAttributeValues)
                query = DbSet.Include(p => p.ProductAttributeValues.Select(pav => pav.ProductAttributeValueTranslations.Select(pavt=>pavt.Language ==language)));


            return query;
        }

    }
}
