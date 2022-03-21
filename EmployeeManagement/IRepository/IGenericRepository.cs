using System.Linq.Expressions;

namespace EmployeeManagement.IRepository
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<IEnumerable<Entity>> All();
        Task<Entity> GetById(Guid id);
        Task<bool> Add(Entity entity);
        Task<bool> Delete(Guid id);
        Task<bool> Upsert(Entity entity);
        Task<IEnumerable<Entity>> Find(Expression<Func<Entity, bool>> predicate);
    }
}
