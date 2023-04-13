using ProvaPub.Models;
using System.Linq.Expressions;

namespace ProvaPub.Contracts
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> FindByIdAsync(int id);
    }
}
