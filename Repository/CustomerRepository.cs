using Microsoft.EntityFrameworkCore;
using ProvaPub.Contracts;
using ProvaPub.Models;

namespace ProvaPub.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(TestDbContext context) : base(context) { }


        public List<Customer> GetPaginated(int page, int totalItems)
        {
            var skip = (page - 1) * totalItems;
            return Db.Customers.Skip(skip).Take(totalItems).OrderBy(x => x.Id).ToList();
        }
    }
}
