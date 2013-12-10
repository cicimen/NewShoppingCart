using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//using ShoppingCart.Domain.Abstract;
//using ShoppingCart.Domain.Entities;
using ShoppingCart.Data.Entity;

namespace ShoppingCart.UI.Controllers
{
    public class NavController : ShoppingCartControllerBase
    {
        //private ICategoryRepository repository;
        //public const string LanguageSessionKey = "Language";
        //public NavController(ICategoryRepository repo)
        //{
        //    repository = repo;
        //}

        [ChildActionOnly]
        public PartialViewResult Menu(string categoryURLText = null)
        {
            ViewBag.SelectedCategory = categoryURLText;
            //Parent kategorisi olmayan kategorileri dönüyor.
            IEnumerable<Category> categories = Categories.GetAllRoot(Languages.GetLanguage()) ;
            return PartialView(categories);
        }

        public ActionResult ChangeLanguage(string ReturnURL = "", string Language = "tr")
        {
            Languages.SetLanguage(Language);
            //string language;
            //if(Session[LanguageSessionKey] != null)
            //language = Session[LanguageSessionKey] as string;

            //Session[LanguageSessionKey] = Language;
            //return RedirectPermanent(ReturnURL);
            return  RedirectToLocal(ReturnURL);
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
	}
}