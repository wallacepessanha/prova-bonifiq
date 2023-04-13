using ProvaPub.Contracts;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductList ListProducts(int page)
        {
            return new ProductList() { Products = _productRepository.GetPaginated(page, 10) };
        }

    }
}
