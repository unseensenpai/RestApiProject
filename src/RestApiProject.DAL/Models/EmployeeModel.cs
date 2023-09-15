using System.ComponentModel.DataAnnotations;

namespace TestProject.DAL.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public int Age { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string IdentityNo { get; set; } = string.Empty;
        public decimal Salary { get; set; }

    }
}
