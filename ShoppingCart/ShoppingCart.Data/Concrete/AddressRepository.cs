using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace ShoppingCart.Data.Concrete
{
    public class AddressRepository :EfRepository<Address>, IAddressRepository
    {
        public AddressRepository(DbContext context, bool sharedContext)
            : base(context, sharedContext) { }

        ///<summary>
        ///This method will be called by administrators.
        ///</summary>
        public Address GetBy(int id)
        {
            return Find(a => a.AddressID == id);
        }

        ///<summary>
        ///This method will be called by application users.
        ///</summary>
        public IEnumerable<Address> GetFor(ApplicationUser user)
        {
            return All().Include(a => a.City).Where(a => a.ApplicationUserID == user.Id).OrderByDescending(a => a.DateCreated);
            //return user.Addresses.OrderByDescending(a => a.DateCreated);
        }

        ///<summary>
        ///This method will be called by application users.
        ///</summary>
        public void AddFor(Address address, ApplicationUser user)
        {
            user.Addresses.Add(address);

            //Context.SaveChanges();
            //if (!ShareContext)
            //{
            //    Context.SaveChanges();
            //}
        }

        ///<summary>
        ///This method will be called by application users.
        ///</summary>
        public void DeleteAddress(int id, ApplicationUser user)
        {
            Address address = user.Addresses.SingleOrDefault(a => a.AddressID == id);
            
            if (address != null)
            {
                Delete(address);
                Context.SaveChanges();


                //if (!ShareContext)
                //{
                //    Context.SaveChanges();
                //}
            }
        }

        ///<summary>
        ///This method will be called by administrators
        ///</summary>
        public void DeleteAddress(int id)
        {
            Address address = Find(a => a.AddressID == id);
            if (address != null)
            {
                Delete(address);
                if (!ShareContext)
                {
                    Context.SaveChanges();
                }
            }
        }

        


    }
}
