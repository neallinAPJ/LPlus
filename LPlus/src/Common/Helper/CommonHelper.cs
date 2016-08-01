
using System.Collections.Generic;
using System.Linq;

namespace Common.Helper
{
    public static class CommonHelper
    {
        public static int CountNotNull<TSource>(this IEnumerable<TSource> source)
        {
            if(source != null)
            {
                return source.Count();
            }
            return 0;
        }
    }
}
