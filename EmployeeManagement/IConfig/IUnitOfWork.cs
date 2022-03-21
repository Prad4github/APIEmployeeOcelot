using EmployeeManagement.IRepository;

namespace EmployeeManagement.IConfig
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employee { get; }
        Task CompleteAsync();
        void Dispose();
    }
}
