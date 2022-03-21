using EmployeeManagement.Config;
using EmployeeManagement.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeManagement.Repository
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        protected EmployeeContext _context;
        internal DbSet<Entity> dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(EmployeeContext context, ILogger logger)
        {
            _context = context;
            this.dbSet = context.Set<Entity>();
            this._logger = logger;
        }

        public virtual Task<IEnumerable<Entity>> All()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<Entity> GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Add(Entity entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Upsert(Entity entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<Entity>> Find(Expression<Func<Entity, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }
    }
}
