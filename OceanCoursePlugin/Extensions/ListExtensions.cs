using System;
using System.Collections.Generic;

namespace OceanCoursePlugin.Extensions
{
    public static class ListExtensions
    {
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int size)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (size <= 0) throw new ArgumentNullException(nameof(size));
            //
            var batch = new List<T>();
            //
            foreach (var item in source)
            {
                batch.Add(item);
                //
                if (batch.Count == size)
                {
                    yield return batch;
                    batch = new List<T>();
                }
            }
            //
            if (batch.Count > 0)
            {
                yield return batch;
            }
        }
    }
}
