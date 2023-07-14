using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestProject.Contracts.Employee;
using TestProject.DAL;
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


        public async Task<BaseDataResponse<IEnumerable<EmployeeResponseDto>>> GetEmployeesAsync()
        {
            try
            {
                _logger.LogInformation("Getting employee's from @Context", _context);
                var dto = await _context.Employee.ToListAsync();
                var result = _mapper.Map<List<EmployeeResponseDto>>(dto);
                return new BaseDataResponse<IEnumerable<EmployeeResponseDto>>() { Data = result, IsSuccess = true, Message = "Success" };
            }
            catch (Exception ex)
            {
                return new BaseDataResponse<IEnumerable<EmployeeResponseDto>> { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}