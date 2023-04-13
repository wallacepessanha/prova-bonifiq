using ProvaPub.Models;

namespace ProvaPub.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetPaginated(int page, int totalItems);
    }
}
