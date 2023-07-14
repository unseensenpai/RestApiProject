using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestProject.Contracts.Employee;
using TestProject.Dto.Core;
using TestProject.Dto.Employee;
using TestProject.HttpApi.Core;

namespace TestProject.HttpApi.Employee
{
    public class EmployeeController : CoreController, IEmployeeService
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet]
        public Task<BaseDataResponse<IEnumerable<EmployeeResponseDto>>> GetEmployeesAsync() 
            => _employeeService.GetEmployeesAsync();
    }
}
