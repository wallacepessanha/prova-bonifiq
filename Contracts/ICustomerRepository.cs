using ProvaPub.Models;

namespace ProvaPub.Contracts
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        List<Customer> GetPaginated(int page, int totalItems);
    }
}
