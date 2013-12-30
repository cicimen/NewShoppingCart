using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ShoppingCart.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingCart.Service.ViewModel
{
    public class CheckoutAddressViewModel
    {
        public int SelectedAddress { get; set; }
        public List<AddressViewModel> Addresses { get; set; }
    }
}
