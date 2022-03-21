using EmployeeManagement.Config;
using EmployeeManagement.IConfig;
using EmployeeManagement.IRepository;
using EmployeeManagement.Repository;

namespace EmployeeManagement.Config
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EmployeeContext _context;
        private readonly ILogger _logger;

        private bool disposed = false;

        public IEmployeeRepository Employee { get; private set; }

        public UnitOfWork(EmployeeContext context, ILoggerFactory loggerFactory)
        {
            _context = context;

            _logger = loggerFactory.CreateLogger("logs");

            Employee = new EmployeeRepository(context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
