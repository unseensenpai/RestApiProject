using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Concrete;
using TestProject.DAL;
using TestProject.DAL.Models;
using TestProject.Dto;
using TestProject.Dto.Core;
using TestProject.HttpApi.Core;

namespace TestProject.HttpApi.Employee
{
    public class EmployeeController : CoreController
    {
        private readonly MySqlContext _context;
        public EmployeeController(MySqlContext context) { _context = context; }

        [HttpPost("PostMessage")]
        public async Task<BaseDataResponse<ResponseDto>> SendMessage(RequestDto requestDto)
        {
            BaseDataResponse<ResponseDto> result = new()
            {
                IsSuccess = await new EmployeeService().GetStringAsync(requestDto.Message),
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
    }
}
