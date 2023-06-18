using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestProject.Contracts.Employee;
using TestProject.DAL;
using TestProject.DAL.Models;
using TestProject.Dto;
using TestProject.Dto.Core;
using TestProject.HttpApi.Core;

namespace TestProject.HttpApi.Employee
{
    public class EmployeeController : CoreController, IEmployeeService
    {
        private readonly MySqlContext _context;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(MySqlContext context, IEmployeeService employeeService)
        {
            _context = context;
            _employeeService = employeeService;
        }

        [HttpPost("PostMessage")]
        public async Task<BaseDataResponse<ResponseDto>> SendMessage(RequestDto requestDto)
        {
            BaseDataResponse<ResponseDto> result = new()
            {
                IsSuccess = await _employeeService.GetEmployeeNumerator(requestDto.Message),
                Message = requestDto.Message,
                Data = new ResponseDto()
                {
                    Id = 1,
                    Name = "Selman Said"
                }
            };
            return result;
        }

        [HttpGet("GetEmployees")]
        public async Task<IEnumerable<EmployeeModel>> GetEmployeeModelAsync()
        {
            return await _context.Employee.ToListAsync();
        }

        [HttpGet("GetEmployeeWithId")]
        public async Task<BaseDataResponse<EmployeeModel>> GetEmployeeWithIdAsync([FromBody] int id)
        {
            BaseDataResponse<EmployeeModel> response = new();
            EmployeeModel? employee = await _context.Employee.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (employee == null)
            {
                response.Data = null;
                response.IsSuccess = false;
                response.Message = "Employee couldnt found.";
            }
            else
            {
                response.Data = employee;
                response.IsSuccess = true;
                response.Message = $"Employee with {employee.Id} id successfully retrieved.";
            }
            return response;
        }

        [HttpGet("[Action]")]
        public Task<bool> GetEmployeeNumerator([FromQuery]string input) => _employeeService.GetEmployeeNumerator(input);
    }
}
