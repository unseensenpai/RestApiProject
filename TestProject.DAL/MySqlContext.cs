using Microsoft.EntityFrameworkCore;
using TestProject.DAL.Models;

namespace TestProject.DAL
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {
            
        }
        public DbSet<EmployeeModel> Employee { get; set; }
    }
}