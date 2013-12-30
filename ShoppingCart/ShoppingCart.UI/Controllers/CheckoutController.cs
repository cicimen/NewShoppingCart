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
    [Authorize]
    public class CheckoutController : ShoppingCartControllerBase
    {
        //
        // GET: /Checkout/
        public ActionResult Address()
        {
            var viewModel = new CheckoutAddressViewModel { Addresses = new List<AddressViewModel>() };
            List<Address> addresses = null;
            if (this.User != null && this.User.Identity.IsAuthenticated)
            {
                ApplicationUser user = UserManager.FindByName(System.Web.HttpContext.Current.User.Identity.Name);
                if (user != null)
                {
                    addresses = Addresses.GetFor(user).ToList();

                }
            }

            int count = 0;
            foreach (var address in addresses)
            {
                if (count == 0)
                {
                    viewModel.SelectedAddress = address.AddressID;
                    count++;
                }
                viewModel.Addresses.Add(new AddressViewModel {
                    AddressDescription = address.AddressDescription,
                    AddressID = address.AddressID,
                    AddressLine1 = address.AddressLine1,
                    AddressLine2 = address.AddressLine2,
                    CityID = address.CityID,
                    NameSurname = address.NameSurname,
                    Phone = address.Phone,
                    PhoneMobile = address.PhoneMobile,
                    PostalCode = address.PostalCode
                });
            }

            return View(viewModel);
        }
	}
}