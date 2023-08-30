using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
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


        /// <summary>
        /// Get "count" of Employees from database.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<BaseDataResponse<IEnumerable<EmployeeResponseDto>>> GetEmployeesAsync([Required, FromQuery] int count)
            => await _employeeService.GetEmployeesAsync(count);

        /// <summary>
        /// Save employees to database if not exist.
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddEmployeesAsync([FromBody, Required] IEnumerable<EmployeeRequestDto> employees)
            => await _employeeService.AddEmployeesAsync(employees);

        /// <summary>
        /// Generate random employees
        /// if isSavedToDatabase is true records will saved to database.
        /// </summary>
        /// <param name="employeeNumberToGenerate"></param>
        /// <param name="isSavedToDatabase"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IEnumerable<EmployeeResponseDto>> GenerateEmployees([Required, FromQuery] int employeeNumberToGenerate, [FromQuery] bool isSavedToDatabase = false)
            => await _employeeService.GenerateEmployees(employeeNumberToGenerate, isSavedToDatabase);

        /// <summary>
        /// Get employee information by job start and end date with count filter.
        /// </summary>
        /// <param name="count">How many records you need?</param>
        /// <param name="startDate">Job start date.</param>
        /// <param name="endDate">Job end date.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<BaseDataResponse<IEnumerable<EmployeeResponseDto>>> GetEmployeeSalariesByDate([Required, FromQuery] int count, [Required, FromQuery] DateTime startDate, [Required, FromQuery] DateTime endDate)
            => await _employeeService.GetEmployeeSalariesByDate(count, startDate, endDate);
    }
}
