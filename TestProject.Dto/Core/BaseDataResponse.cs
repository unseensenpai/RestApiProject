using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Dto.Core
{
    public class BaseDataResponse<T> where T : class
    {
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = false;
    }
}
