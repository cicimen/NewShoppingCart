using ShoppingCart.Data.Entity;
using System.Collections.Generic;

namespace ShoppingCart.Service
{
    public interface IAddressService
    {
        IEnumerable<Address> GetFor(ApplicationUser user);
        void AddFor(Address address, ApplicationUser user);
        void DeleteAddress(int id, ApplicationUser user);
    }
}
