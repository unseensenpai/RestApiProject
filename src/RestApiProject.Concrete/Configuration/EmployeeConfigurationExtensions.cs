using Microsoft.Extensions.DependencyInjection;
using TestProject.Concrete.Employee;
using TestProject.Contracts.Employee;

namespace TestProject.Concrete.Configuration
{
    public static class EmployeeConfigurationExtensions
    {
        public static IServiceCollection AddEmployeeModule(this IServiceCollection services) => 
            services
                .AddTransient<IEmployeeService, EmployeeService>();
    }
}
