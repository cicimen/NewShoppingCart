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
    public class CheckoutController : ShoppingCartControllerBase
    {
        public ActionResult Address()
        {
            //bu aşağıdaki cart nesnesi yaratılsın diye yapılıyor.
            Carts.GetCart(this.HttpContext);
            //TODO: Eğer sepet boşsa sepete dön
            if(Carts.GetCount() <1 )
            {
                return RedirectToAction("Index", "Cart", null);
            }

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
                if (count == 0 && Session["CheckoutAddress"] == null)
                {
                    Session["CheckoutAddress"] = address;
                    viewModel.SelectedAddressID = address.AddressID;
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
            //eğer session'da seçilmiş bir adres varsa dropdownlist'te onu seçili yap
            if (Session["CheckoutAddress"] != null)
            {
                var address = (Address)Session["CheckoutAddress"];
                viewModel.SelectedAddressID = address.AddressID;
                count++;
            }


            return View(viewModel);
        }

        public ActionResult Shipment()
        {
            //bu aşağıdaki cart nesnesi yaratılsın diye yapılıyor.
            Carts.GetCart(this.HttpContext);
            //TODO: Eğer sepet boşsa sepete dön
            if (Session["CheckoutAddress"] == null)
            {
                return RedirectToAction("Address", "Checkout", null);
            }
            List<ShippingMethod> shippingMethods = ShippingMethods.All().Where(sm => sm.Enabled == true).ToList();

            int selectedShipmentMethod = 0;
            if (Session["ShippingMethod"] != null)
            {
                selectedShipmentMethod = ((ShippingMethod)Session["ShippingMethod"]).ShippingMethodID;
            }
            
            var checkoutShipmentViewModel = new CheckoutShipmentViewModel { 
            ShipmentMethods = shippingMethods,
            SelectedShipmentID = selectedShipmentMethod
            };
            return View(checkoutShipmentViewModel);
        }

        public ActionResult Payment()
        {
            //bu aşağıdaki cart nesnesi yaratılsın diye yapılıyor.
            Carts.GetCart(this.HttpContext);
            if (Carts.GetCount() < 1)
            {
                return RedirectToAction("Index", "Cart", null);
            }
            else if (Session["CheckoutAddress"] == null)
            {
                return RedirectToAction("Address", "Checkout", null);
            }
            else
            {
                List<PaymentMethod> paymentMethods = PaymentMethods.All().Where(pm => pm.Enabled == true).ToList();
                return View(paymentMethods);
            }
            
        }

        [AjaxOrChildActionOnlyAttribute]
        public ActionResult SelectAddress(int selectedAddressID)
        {
            if (this.User != null && this.User.Identity.IsAuthenticated)
            {
                ApplicationUser user = UserManager.FindByName(System.Web.HttpContext.Current.User.Identity.Name);
                if (user != null)
                {
                    Address address = Addresses.GetFor(user).Where(a=> a.AddressID == selectedAddressID).SingleOrDefault();
                    Session["CheckoutAddress"] = address;
                    return PartialView("_SelectedAddress", address);
                }
                return PartialView("_SelectedAddress",null);
            }
            return null;
        }

        [HttpPost]
        [AjaxOrChildActionOnlyAttribute]
        public ActionResult SelectShipment(int selectedShipmentID)
        {
            if (this.User != null && this.User.Identity.IsAuthenticated)
            {
                ShippingMethod shippingMethod = ShippingMethods.All().SingleOrDefault(sm => sm.ShippingMethodID == selectedShipmentID);
                if(shippingMethod != null)
                {
                    Session["ShippingMethod"] = shippingMethod;
                    return null;
                }
            }
            return null;
        }



        [AjaxOrChildActionOnly]
        public ActionResult CartCheckoutSummary()
        {
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = Carts.GetCart(this.HttpContext).GetCartItems(),
                CartTotal = Carts.GetTotal(),
                CartCount = Carts.GetCount()
            };
            return PartialView("_CartCheckoutSummary", viewModel);
        }

        [AjaxOrChildActionOnly]
        public ActionResult AddressCheckoutSummary()
        {
            Address address = (Address)Session["CheckoutAddress"] ;
            return PartialView("_AddressCheckoutSummary", address);
        }

    }
}