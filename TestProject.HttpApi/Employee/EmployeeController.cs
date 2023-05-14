using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Concrete;
using TestProject.Dto;
using TestProject.Dto.Core;
using TestProject.HttpApi.Core;

namespace TestProject.HttpApi.Employee
{
    public class EmployeeController : CoreController
    {
        public EmployeeController() { }

        [HttpPost("PostMessage")]
        public async Task<BaseDataResponse<ResponseDto>> SendMessage(RequestDto requestDto)
        {
            var result = new BaseDataResponse<ResponseDto>
            {
                IsSuccess = await new EmployeeService().GetStringAsync(requestDto.Message),
                Message = requestDto.Message,
                Data = new ResponseDto()
                {
                    Id = 1,
                    Name = "Selman Said"
                }
            };
            return result;
        }
    }
}
