using ProvaPub.Contracts;
using ProvaPub.Models;

namespace ProvaPub.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(TestDbContext context) : base(context) { }

    }
}
