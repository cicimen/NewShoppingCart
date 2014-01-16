using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ShoppingCart.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingCart.Service.ViewModel
{
    public class CheckoutShipmentViewModel
    {
        public int SelectedShipmentID { get; set; }
        public List<ShippingMethod> ShipmentMethods { get; set; }
    }
}
