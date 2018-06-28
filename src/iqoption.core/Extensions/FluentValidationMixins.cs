using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace iqoption.core.Extensions {
    public static class FluentValidationMixins {
        public static string GetErrorDescriptions(this IEnumerable<ValidationFailure> errors) {
            return string.Join(", ", errors.Select(x => x.ErrorMessage));
        }
    }
}