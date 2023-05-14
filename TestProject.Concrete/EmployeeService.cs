using TestProject.Constants;
using TestProject.Contracts;

namespace TestProject.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        public async Task<bool> GetStringAsync(string input)
        {
            try
            {
                await Console.Out.WriteLineAsync($"{input} - {EnumValues.Said}");
                return true;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"{ex.Message}");
                return false;
            }
        }
    }
}