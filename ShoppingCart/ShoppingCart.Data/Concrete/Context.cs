using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ShoppingCart.Data.Abstract;

using Microsoft.AspNet.Identity;
using ShoppingCart.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ShoppingCart.Data.Concrete
{
    public class Context : IContext
    {
        private readonly DbContext _db;

        public Context(DbContext context = null, IAddressRepository addresses = null, ICartRepository carts = null, ICategoryRepository categories = null,ILanguageRepository languages = null,
             IOrderItemRepository orderItems=null,ICityRepository cities = null,IProductRepository products = null, IProductImageRepository productImages = null
            ,IShippingMethodRepository shippingMethods = null)
        {
            _db = context ?? new ApplicationDbContext();
            Addresses = addresses ?? new AddressRepository(_db, true);
            Carts = carts ?? new CartRepository(_db, true);
            Categories = Categories ?? new CategoryRepository(_db, true);
            Cities = cities ?? new CityRepository(_db, true);
            Languages = languages ?? new LanguageRepository(_db, true);
            OrderItems = orderItems ?? new OrderItemRepository(_db, true);
            Products = products ?? new ProductRepository(_db, true);
            ProductImages = productImages?? new ProductImageRepository(_db, true);
            ShippingMethods = shippingMethods ?? new ShippingMethodRepository(_db, true);
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
        }


        public IAddressRepository Addresses
        {
            get;
            private set;
        }
        public ICartRepository Carts
        {
            get;
            private set;
        }
        public ICategoryRepository Categories
        {
            get;
            private set;
        }
        public ICityRepository Cities
        {
            get;
            private set;
        }
        public ILanguageRepository Languages
        {
            get;
            private set;
        }
        public IOrderItemRepository OrderItems
        {
            get;
            private set;
        }
        public IProductRepository Products
        {
            get;
            private set;
        }
        public IProductImageRepository ProductImages
        {
            get;
            private set;
        }

        public IShippingMethodRepository ShippingMethods
        {
            get;
            private set;
        }


        public UserManager<ApplicationUser> UserManager
        {
            get;
            private set;
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            if (_db != null)
            {
                try
                {
                    _db.Dispose();
                }
                catch { }
            }
        }
    }
}