using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiProject.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string? value) 
            => string.IsNullOrEmpty(value) && string.IsNullOrWhiteSpace(value);
    }
}
