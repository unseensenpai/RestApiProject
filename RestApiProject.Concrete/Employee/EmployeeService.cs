using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestApiProject.Dto.Employee;
using TestProject.Contracts.Employee;
using TestProject.DAL;
using TestProject.DAL.Models;
using TestProject.Dto.Core;
using TestProject.Dto.Employee;


namespace TestProject.Concrete.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly MySqlContext _context;
        private readonly IMapper _mapper;
        readonly ILogger<EmployeeService> _logger;
        public EmployeeService(MySqlContext context, IMapper mapper, ILogger<EmployeeService> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }


        public async Task<BaseDataResponse<IEnumerable<EmployeeResponseDto>>> GetEmployeesAsync(int count)
        {
            try
            {
                _logger.LogInformation("Getting employee's from {context}", _context);
                var dto = await _context.Employee.Take(count).ToListAsync();
                //dto.ForEach(employee =>
                //{
                //    employee.Age = new Random().Next(1, 100);
                //    employee.StartDate = new DateTime(new Random().Next(1000, 9999), new Random().Next(1, 12), new Random().Next(1, 27));
                //    employee.EndDate = new DateTime(new Random().Next(1000, 9999), new Random().Next(1, 12), new Random().Next(1, 27));
                //    employee.Name = $"Selman{new Random().Next(1, 1000000000)}";
                //    employee.Surname = $"Gulmez{new Random().Next(1, 1000000000)}";
                //    employee.Salary = new Random().Next(40000, 1000000000);
                //    employee.IdentityNo = new Random().Next(1111111111, int.MaxValue).ToString();
                //});
                var result = _mapper.Map<List<EmployeeResponseDto>>(dto);

                _logger.LogInformation("Employees: {identities} - {name}", result.Select(x => x.IdentityNo), result.Select(x => x.Name));

                return new BaseDataResponse<IEnumerable<EmployeeResponseDto>>() { Data = result, IsSuccess = true, Message = "Success" };
            }
            catch (Exception ex)
            {
                return new BaseDataResponse<IEnumerable<EmployeeResponseDto>> { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<bool> AddEmployeesAsync(IEnumerable<EmployeeRequestDto> employees)
        {
            try
            {
                List<EmployeeModel> employeeModels = new();
                employeeModels.AddRange(_mapper.Map<IEnumerable<EmployeeModel>>(employees));
                await _context.Employee.AddRangeAsync(employeeModels);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical("An error occured while adding {employees} - ERROR: {ex}", employees, ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<EmployeeResponseDto>> GenerateEmployees(int employeeNumberToGenerate, bool isSavedToDatabase)
        {
            List<EmployeeResponseDto> employeeModels = new();
            for (int i = 0; i < employeeNumberToGenerate; i++)
            {
                EmployeeResponseDto employeeResponseDto = new();
                employeeResponseDto.Age = new Random().Next(1, 100);
                employeeResponseDto.StartDate = new DateTime(new Random().Next(1000, 9999), new Random().Next(1, 12), new Random().Next(1, 27));
                employeeResponseDto.EndDate = new DateTime(new Random().Next(1000, 9999), new Random().Next(1, 12), new Random().Next(1, 27));
                employeeResponseDto.Name = $"Selman{new Random().Next(1, 1000000000)}";
                employeeResponseDto.Surname = $"Gulmez{new Random().Next(1, 1000000000)}";
                employeeResponseDto.Salary = new Random().Next(40000, 1000000);
                employeeResponseDto.IdentityNo = new Random().Next(1111111111, int.MaxValue).ToString();
                employeeModels.Add(employeeResponseDto);
            }
            if (isSavedToDatabase)
            {
                await _context.Employee.AddRangeAsync(_mapper.Map<IEnumerable<EmployeeModel>>(employeeModels));
                await _context.SaveChangesAsync();
            }
            
            return employeeModels;
        }

        public async Task<BaseDataResponse<IEnumerable<EmployeeResponseDto>>> GetEmployeeSalariesByDate(EmployeeParameters parameters)
        {
            try
            {
                _logger.LogInformation("Getting employee salaries from {context}", _context);
                var dto = await _context.Employee
                    .Where(x =>
                        x.StartDate >= parameters.StartDate.Date &&
                        x.EndDate < parameters.EndDate.AddDays(1).Date &&
                        x.Salary >= parameters.SalaryFrom &&
                        x.Salary < parameters.SalaryTo)
                    .Take(parameters.RecordCount)
                    .ToListAsync();

                var result = _mapper.Map<List<EmployeeResponseDto>>(dto);

                _logger.LogInformation("Employees: {identities} - {name}", result.Select(x => x.IdentityNo), result.Select(x => x.Name));

                return new BaseDataResponse<IEnumerable<EmployeeResponseDto>>() { Data = result, IsSuccess = true, Message = "Success" };
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while getting {count} employees information - ERROR: {ex}", parameters.RecordCount, ex.Message);
                return new BaseDataResponse<IEnumerable<EmployeeResponseDto>> { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}