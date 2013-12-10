using ShoppingCart.Data.Entity;
using System.Collections.Generic;
using ShoppingCart.Service.ViewModel;

namespace ShoppingCart.Service
{
    public interface IAddressService
    {
        IEnumerable<Address> GetFor(ApplicationUser user);
        void AddFor(AddressViewModel address, ApplicationUser user);
        void DeleteAddress(int id, ApplicationUser user);
    }
}
