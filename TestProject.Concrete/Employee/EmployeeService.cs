using TestProject.Constants;
using TestProject.Contracts.Employee;

namespace TestProject.Concrete.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeService(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public async Task<bool> GetEmployeeNumerator(string input)
        {
            try
            {
                var x = _employeeService.GetEmployeeNumerator(input);
                Console.WriteLine(x);
                await Console.Out.WriteLineAsync($"{input} - {EnumValues.Said}");
                return true;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"{ex.Message}");
                return false;
            }
        }
    }
}