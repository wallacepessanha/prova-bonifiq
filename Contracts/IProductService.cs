using ProvaPub.Models;

namespace ProvaPub.Contracts
{
    public interface IProductService
    {
        ProductList ListProducts(int page);
    }
}
