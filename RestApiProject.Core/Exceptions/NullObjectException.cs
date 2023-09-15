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
    public class NullObjectException : Exception
    {
        public NullObjectException()
        {
        }

        public NullObjectException(string message) : base(message)
        {
        }

        public NullObjectException(string message, Exception inner) : base(message, inner)
        {
        }

        public static void ThrowIfNull([NotNull] object? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument.IsNull())
            {
                throw new($"The database connection gave an error. The return was null, empty or white space. PARAMETER: {paramName}");
            }
        }
    }
}
