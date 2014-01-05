using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using ShoppingCart.Domain.Abstract;
using ShoppingCart.Service.ViewModel;
using ShoppingCart.Data.Concrete;
using ShoppingCart.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ShoppingCart.UI.Helpers;

namespace ShoppingCart.UI.Controllers
{
    //[RequireHttps] 
    [Authorize]
    public class AddressController : ShoppingCartControllerBase
    {
        public ActionResult Index()
        {
            List<Address> addresses = null;
            if (this.User != null && this.User.Identity.IsAuthenticated)
            {
                ApplicationUser user = UserManager.FindByName(System.Web.HttpContext.Current.User.Identity.Name);
                if (user != null)
                {
                    addresses = Addresses.GetFor(user).ToList();
                   
                }
            }
            //TODO: bu gerekmiyecek sanırım.
            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("_AddressTable", addresses);
            //}

            return View(addresses);
        }


        //[ChildActionOnly]
        [AjaxOrChildActionOnlyAttribute]
        public ActionResult Create()
        {
            //ViewBag.CityID = new SelectList(Cities.All().OrderBy(x=>x.DisplayOrder).ThenBy(x=>x.CityName).ToList() , "CityID", "CityName");
            var addressViewModel = new AddressViewModel
            {
                Cities = Cities.All().OrderBy(x => x.DisplayOrder).ThenBy(x => x.CityName).ToList()
            };
            return View("_Create", addressViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddressViewModel address)
        {
            if (!ModelState.IsValid)
            {
                address.Cities = Cities.All().OrderBy(x => x.DisplayOrder).ThenBy(x => x.CityName).ToList();
                return PartialView("_Create",address);
            }
            
            else
            {
                if (this.User != null && this.User.Identity.IsAuthenticated)
                {
                    ApplicationUser user = UserManager.FindByName(System.Web.HttpContext.Current.User.Identity.Name);
                    if (user != null)
                    {
                        address.ApplicationUserID = user.Id;
                        Addresses.AddFor(address,user);

                        //if (Request.IsAjaxRequest())
                        //{
                        //    IEnumerable<Address> addresses = Addresses.GetFor(user); 
                        //    return PartialView("_AddressTable", addresses);
                        //}
                        return null;
                        //return RedirectToAction("Index", "Address", null);
                    }
                    else
                    { 
                        return RedirectToActionPermanent("Index", "Home");
                    }                
                }
                return RedirectToActionPermanent("Index", "Home");
            }             

        }



        [AjaxOrChildActionOnlyAttribute]
        public ActionResult Edit(int id)
        {
            ApplicationUser user = UserManager.FindByName(System.Web.HttpContext.Current.User.Identity.Name);
            Address address = user.Addresses.SingleOrDefault(a=>a.AddressID == id);
            if(address == null)
            {
                return null;
            }
            var addressViewModel = new AddressViewModel
            {
                AddressID = address.AddressID,
                AddressDescription = address.AddressDescription,
                AddressLine1 = address.AddressLine1,
                AddressLine2 =address.AddressLine2,               
                NameSurname = address.NameSurname,
                CityID = address.CityID,
                Phone = address.Phone,
                PhoneMobile = address.PhoneMobile,
                PostalCode = address.PostalCode,
                Cities = Cities.All().OrderBy(x => x.DisplayOrder).ThenBy(x => x.CityName).ToList()
            };
            return View("_Edit", addressViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddressViewModel address)
        {
            if (!ModelState.IsValid)
            {
                address.Cities = Cities.All().OrderBy(x => x.DisplayOrder).ThenBy(x => x.CityName).ToList();
                return PartialView("_Edit", address);
            }

            else
            {
                if (this.User != null && this.User.Identity.IsAuthenticated)
                {
                    ApplicationUser user = UserManager.FindByName(System.Web.HttpContext.Current.User.Identity.Name);
                    Address userAddress = user.Addresses.FirstOrDefault(a => a.AddressID == address.AddressID);
                    if (userAddress != null)
                    {
                        userAddress.AddressDescription = address.AddressDescription;
                        userAddress.AddressLine1 = address.AddressLine1;
                        userAddress.AddressLine2 = address.AddressLine2;
                        userAddress.CityID = address.CityID;
                        userAddress.NameSurname = address.NameSurname;
                        userAddress.Phone = address.Phone;
                        userAddress.PhoneMobile = address.PhoneMobile;
                        userAddress.PostalCode = address.PostalCode;
                        Addresses.UpdateAddress(userAddress.AddressID, user, userAddress);

                        if (Session["CheckoutAddress"] != null)
                        {
                            Address sessionAddress = (Address) Session["CheckoutAddress"] ;
                            if(sessionAddress.AddressID == userAddress.AddressID)
                            {
                                Session["CheckoutAddress"] = userAddress;
                            }
                        }


                        return null;
                    }
                    else
                    {
                        return RedirectToActionPermanent("Index", "Home");
                    }
                }
                return RedirectToActionPermanent("Index", "Home");
            }   

        }

        [HttpGet]
        [AjaxOrChildActionOnlyAttribute]
        public ActionResult Delete(int id)
        {
            ApplicationUser user = UserManager.FindByName(System.Web.HttpContext.Current.User.Identity.Name);
            Address address = user.Addresses.SingleOrDefault(a => a.AddressID == id);

            var addressViewModel = new AddressViewModel
            {
                AddressID = address.AddressID,
                AddressDescription = address.AddressDescription,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                NameSurname = address.NameSurname,
                CityID = address.CityID,
                Phone = address.Phone,
                PhoneMobile = address.PhoneMobile,
                PostalCode = address.PostalCode,
                Cities = Cities.All().OrderBy(x => x.DisplayOrder).ThenBy(x => x.CityName).ToList()
            };


            return View("_Delete", addressViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(AddressViewModel address)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Delete", new AddressViewModel { });
            }

            else
            {
                if (this.User != null && this.User.Identity.IsAuthenticated)
                {
                    ApplicationUser user = UserManager.FindByName(System.Web.HttpContext.Current.User.Identity.Name);
                    Address userAddress = user.Addresses.FirstOrDefault(a => a.AddressID == address.AddressID);
                    if (userAddress != null)
                    {
                        Addresses.DeleteAddress(userAddress.AddressID, user);
                        if (Session["CheckoutAddress"] != null)
                        {
                            Address sessionAddress = (Address)Session["CheckoutAddress"];
                            if (sessionAddress.AddressID == userAddress.AddressID)
                            {
                                Session["CheckoutAddress"] = null;
                            }
                        }
                        return null;
                    }
                    else
                    {
                        return RedirectToActionPermanent("Index", "Home");
                    }
                }
                return RedirectToActionPermanent("Index", "Home");
            }

        }

	}
}