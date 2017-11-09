using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Podskal.SystemExt.LinqExt
{
    /// <summary>
    /// Contains additional LINQ extension methods.
    /// </summary>
    public static class EnumerableExt
    {
        #region Resource management and Dispose helpers

        /// <summary>
        /// Creates an IDisposable that will dispose all items in the collection.
        /// </summary>
        /// <typeparam name="TDisposableItem"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static IDisposable ItemDisposal<TDisposableItem>(this IEnumerable<TDisposableItem> collection)
            where TDisposableItem : IDisposable
        {
            return new ItemsInCollectionDisposable<TDisposableItem>(
                collection, 
                (item) => 
                    item.Dispose());
        }
        
        #endregion


        #region Stream processing and grouping helpers

        /// <summary>
        /// Zips collections of equal lengths. Otherwise fails with exception.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="zip"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> ZipStrict<T1, T2, TResult>(this IEnumerable<T1> first, IEnumerable<T2> second, Func<T1, T2, TResult> zip)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }
            if (second == null)
            {
                throw new ArgumentNullException("second");
            }
            if (zip == null)
            {
                throw new ArgumentNullException("zip");
            }

            using (var firstEnumerator = first.GetEnumerator())
            {
                using (var secondEnumerator = second.GetEnumerator())
                {
                    while (true)
                    {
                        var movedFirst = firstEnumerator.MoveNext();
                        var movedSecond = secondEnumerator.MoveNext();

                        if ((movedFirst) && (movedSecond))
                        {
                            yield return zip(firstEnumerator.Current, secondEnumerator.Current);
                        }
                        else if ((!movedFirst) && (!movedSecond))
                        {
                            yield break;
                        }
                        else
                        {
                            throw new ArgumentException("The enumerables have different amount of elements");
                        }
                    }
                }
            }
        }
        

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> collection, Func<T, T, Boolean> compare, Func<T, int> hash)
        {
            return collection.Distinct(new DelegateEqualityComparer<T>(compare, hash));
        }


        /// <summary>
        /// Groups items by key in the same order as they appear in the sequence. 
        /// For example group odd and even on {0,2,1,3,4,6,11} will become 
        /// {
        ///     {even, {0, 2}},
        ///     {odd, {1, 3}},
        ///     {even, {4, 6}},
        ///     {odd, {11}}
        /// }
        /// </summary>
        public static IEnumerable<KeyValuePair<TKey, IEnumerable<T>>> StreamGroup<T, TKey>(
			this IEnumerable<T> collection,
            Func<T, TKey> keySelector,
            IEqualityComparer<TKey> comparer = null)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }
            comparer = comparer ?? EqualityComparer<TKey>.Default;

            using (IEnumerator<T> enumerator = collection.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    throw new ArgumentException("The collection is empty", "collection");
                }
                TKey currentKey = keySelector(enumerator.Current);
                ICollection<T> currentValues = new List<T>() { enumerator.Current };
                while (enumerator.MoveNext())
                {
                    TKey newKey = keySelector(enumerator.Current);
                    if (!comparer.Equals(currentKey, newKey))
                    {
                        yield return new KeyValuePair<TKey, IEnumerable<T>>(currentKey, currentValues);
                        currentKey = newKey;
                        currentValues = new List<T>();
                    }
                    currentValues.Add(enumerator.Current);
                }
                yield return new KeyValuePair<TKey, IEnumerable<T>>(currentKey, currentValues);
            }
        }
		
        #endregion


        #region Combinatorics

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> collection, T item)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            return collection.ConcatImpl(item);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<T> ConcatImpl<T>(this IEnumerable<T> collection, T item)
        {
            return collection.Concat(From(item));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> From<T>(T item)
        {
            return new T[] { item };
        }


        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            return collection.CombinationsImpl();
        }

        public static IEnumerable<IEnumerable<T>> CombinationsImpl<T>(this IEnumerable<T> collection)
        {
            List<IEnumerable<T>> previousCombinations = new List<IEnumerable<T>>();
            List<IEnumerable<T>> currentCombinations = new List<IEnumerable<T>>();

            foreach (var item in collection)
            {
                currentCombinations.Add(From(item));

                foreach (var prevCombination in previousCombinations)
                {
                    currentCombinations.Add(prevCombination.ConcatImpl(item));
                }

                foreach (var curCombination in currentCombinations)
                {
                    yield return curCombination;
                }

                previousCombinations.AddRange(currentCombinations);
                currentCombinations.Clear();
            }
        }

        #endregion
    }
}
