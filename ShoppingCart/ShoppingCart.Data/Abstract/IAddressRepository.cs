using System.Linq;
using ShoppingCart.Data.Entity;
using System.Collections.Generic;

namespace ShoppingCart.Data.Abstract
{
    public interface IAddressRepository :IRepository<Address>
    {
        Address GetBy(int id);
        IEnumerable<Address> GetFor(ApplicationUser user);
        void AddFor(Address address, ApplicationUser user);
        void DeleteAddress(int id, ApplicationUser user);
        void DeleteAddress(int id);
    }
}
