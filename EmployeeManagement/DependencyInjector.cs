using Autofac;
using EmployeeManagement.Config;
using EmployeeManagement.IConfig;
using EmployeeManagement.IRepository;
using EmployeeManagement.Model;
using EmployeeManagement.Repository;

namespace EmployeeManagement
{
    public class DependencyInjector : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType(typeof(UnitOfWork))
            //    .As<IUnitOfWork>()
            //    .InstancePerLifetimeScope();
        }
    }
}
