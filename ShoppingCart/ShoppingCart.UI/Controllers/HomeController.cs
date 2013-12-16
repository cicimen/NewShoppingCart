using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//sil sonra
using System.Data.Entity;
using ShoppingCart.Data.Entity;
using ShoppingCart.Service.ViewModel;

//using ShoppingCart.Domain.Abstract;
//using ShoppingCart.Domain.Entities;
//using ShoppingCart.UI.Models;

namespace ShoppingCart.UI.Controllers
{
    public class HomeController : ShoppingCartControllerBase
    {

        //private IProductRepository productRepository;

        //TODO: Dışarıdan parametre olarak al bunu
        public int PageSize = 5;
        //public HomeController(IProductRepository productRepository)
        //{
        //    this.productRepository = productRepository;
        //}

        public ActionResult Index(string categoryURLText , int page =1)
        {
            var model = Products.GetProductListViewModel(Languages.GetLanguage(), categoryURLText, page, PageSize);

            //ProductsListViewModel model = new ProductsListViewModel
            //{
            //    Products = Products.GetByCategoryForHome(Languages.GetLanguage(), categoryURLText, (page - 1) * PageSize, PageSize),
            //    PagingInfo = new PagingInfo
            //    {
            //        CurrentPage = page,
            //        ItemsPerPage = PageSize,
            //        TotalItems = Products.GetCountByCategoryForHome(Languages.GetLanguage(),categoryURLText)
            //    },
            //    CurrentCategory = categoryURLText
            //};

            return View(model);
        }


        

	}
}