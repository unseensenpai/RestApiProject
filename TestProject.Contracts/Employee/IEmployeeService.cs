using TestProject.Dto.Core;
using TestProject.Dto.Employee;

namespace TestProject.Contracts.Employee
{
    public interface IEmployeeService
    {
        public Task<BaseDataResponse<IEnumerable<EmployeeResponseDto>>> GetEmployeesAsync();
    }
}
