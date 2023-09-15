using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiProject.Dto.Employee
{
    public record EmployeeParameters
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal SalaryFrom { get; set; }
        public decimal SalaryTo { get; set; }
        public int RecordCount { get; set; }
    }
}
