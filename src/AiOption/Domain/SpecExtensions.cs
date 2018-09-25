using System.Collections.Generic;
using EventFlow.Specifications;

namespace AiOption.Domain
{
    public static class SpecExtensions
    {
        public static ISpecification<IEnumerable<T>> ForAll<T>(this ISpecification<T> specification)
        {
            return new ForAllSpecification<T>(specification);
        }
    }
}