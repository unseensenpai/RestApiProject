using AutoMapper;
using TestProject.DAL.Models;
using TestProject.Dto.Employee;

namespace TestProject.BL.Employee
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeModel, EmployeeResponseDto>().ReverseMap();
        }
    }
}
