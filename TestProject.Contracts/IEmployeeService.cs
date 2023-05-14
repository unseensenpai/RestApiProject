namespace TestProject.Contracts
{
    public interface IEmployeeService
    {
        public Task<bool> GetStringAsync(string input);
    }
}
