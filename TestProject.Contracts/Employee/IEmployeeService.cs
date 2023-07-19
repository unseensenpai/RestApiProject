using TestProject.Dto.Core;
using TestProject.Dto.Employee;

namespace TestProject.Contracts.Employee
{
    public interface IEmployeeService
    {
        public Task<BaseDataResponse<IEnumerable<EmployeeResponseDto>>> GetEmployeesAsync(int count);
        public Task<bool> AddEmployeesAsync(IEnumerable<EmployeeRequestDto> employees);
        public Task<IEnumerable<EmployeeResponseDto>> GenerateEmployees(int employeeNumberToGenerate, bool isSavedToDatabase);
    }
}
