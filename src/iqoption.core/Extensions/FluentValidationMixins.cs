using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iqoption.core.Extensions
{
    public static class FluentValidationMixins
    {
        public static string GetErrorDescriptions(this IEnumerable<FluentValidation.Results.ValidationFailure> errors){
            return string.Join(", ", errors.Select(x => x.ErrorMessage));
        }
    }
}
