using System;
using System.Collections.Generic;

namespace Application.Scripts.Library.Extensions
{
    public static class LinqExtensions
    {
        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return CompareBy(source, keySelector, i => i < 0);
        }
        
        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return CompareBy(source, keySelector, i => i > 0);
        }
        
        private static TSource CompareBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, Predicate<int> predicate)
        {
            using var enumerator = source.GetEnumerator();
            enumerator.MoveNext();
            (TSource source, TKey key) value = (enumerator.Current, keySelector(enumerator.Current));
            
            var comparer = Comparer<TKey>.Default;
            while (enumerator.MoveNext())
            {
                TKey temp = keySelector(enumerator.Current);
                if (predicate(comparer.Compare(temp, value.key)))
                {
                    value = (enumerator.Current, temp);
                }
            }

            return value.source;
        }
        
    }
}