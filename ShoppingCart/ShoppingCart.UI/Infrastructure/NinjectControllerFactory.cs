﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Configuration;

using ShoppingCart.Domain.Abstract;
using ShoppingCart.Domain.Concrete;

namespace ShoppingCart.UI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            ninjectKernel.Bind<ICategoryRepository>().To<EFCategoryRepository>();
            ninjectKernel.Bind<IProductImageRepository>().To<EFProductImageRepository>();
            ninjectKernel.Bind<IAddressRepository>().To<EFAddressRepository>();
           
            //ninjectKernel.Bind<ICartRepository>().To<EFCartRepository>();
        }
    }
}