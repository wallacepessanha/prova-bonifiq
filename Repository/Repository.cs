using Microsoft.EntityFrameworkCore;
using ProvaPub.Contracts;
using ProvaPub.Models;
using System.Linq.Expressions;

namespace ProvaPub.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly TestDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(TestDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task<TEntity?> FindByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().CountAsync(predicate);
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
