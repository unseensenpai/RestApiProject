namespace TestProject.Contracts.Employee
{
    public interface IEmployeeService
    {
        public Task<bool> GetEmployeeNumerator(string input);
    }
}
