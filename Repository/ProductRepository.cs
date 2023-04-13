using ProvaPub.Contracts;
using ProvaPub.Models;
using System.Drawing.Printing;

namespace ProvaPub.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(TestDbContext context) : base(context) { }

        public List<Product> GetPaginated(int page, int totalItems)
        {
            var skip = (page - 1) * totalItems;
            return Db.Products.Skip(skip).Take(totalItems).OrderBy(x=> x.Id).ToList();
        }
    }
}
