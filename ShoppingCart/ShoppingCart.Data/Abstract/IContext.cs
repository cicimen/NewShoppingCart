using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using ShoppingCart.Data.Entity;

namespace ShoppingCart.Data.Abstract
{
    public interface IContext : IDisposable
    {
        IAddressRepository Addresses { get; }
        ICartRepository Carts { get; }
        ICategoryRepository Categories { get; }
        ICityRepository Cities { get; }
        ILanguageRepository Languages { get; }
        IOrderItemRepository OrderItems { get; }
        IProductRepository Products { get; }
        IProductImageRepository ProductImages { get; }
        UserManager<ApplicationUser> UserManager { get; }

        int SaveChanges();
    }
}