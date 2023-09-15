using RestApiProject.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RestApiProject.Core.Exceptions
{
    public class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException()
        {
        }

        public DatabaseConnectionException(string message) : base(message)
        {
        }

        public DatabaseConnectionException(string message, Exception inner) : base(message, inner)
        {
        }

        public static void ThrowIfNullOrEmptyOrWhiteSpace([NotNull] string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument.IsNullOrEmptyOrWhiteSpace())
            {
                throw new DatabaseConnectionException($"The database connection gave an error. The return was null, empty or white space. PARAMETER: {paramName}");
            }
        }        

    }
}
