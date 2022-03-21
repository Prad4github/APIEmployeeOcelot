using EmployeeManagement.Config;
using EmployeeManagement.IRepository;
using EmployeeManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Employee>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(EmployeeRepository));
                return new List<Employee>();
            }
        }

        public override async Task<bool> Upsert(Employee employee)
        {
            try
            {
                if(employee.Id == Guid.Empty)
                {
                    return await Add(employee);
                }
                  
                var existingEmployee = await dbSet.Where(x => x.Id == employee.Id)
                                                    .FirstOrDefaultAsync();

                if (existingEmployee == null)
                    return await Add(employee);

                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Email = employee.Email;
                existingEmployee.PhoneNumber = employee.PhoneNumber;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(EmployeeRepository));
                return false;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var employee = await dbSet.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (employee == null) return false;

                dbSet.Remove(employee);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(EmployeeRepository));
                return false;
            }
        }
    }
}
