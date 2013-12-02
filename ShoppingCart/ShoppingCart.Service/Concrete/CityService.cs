using System.Collections.Generic;
using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;

namespace ShoppingCart.Service
{
    public class CityService : ICityService
    {
        private readonly IContext _context;
        private readonly ICityRepository _cities;

        public CityService(IContext context)
        {
            _context = context;
            _cities = context.Cities;
        }

        public IEnumerable<City> All()
        {
            return _cities.All();
        }
    }
}
