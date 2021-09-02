using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Domain.Extensions
{
    public static class EnumerableExtensions
    {
        public static IOrderedEnumerable<TSource> OrderBy<TSource>(this IEnumerable<TSource> source, string propertyName)
        {
            var prop = TypeDescriptor.GetProperties(typeof(TSource)).Find(propertyName ?? string.Empty, true);

            return source.OrderBy(x => prop?.GetValue(x));
        }
    }
}