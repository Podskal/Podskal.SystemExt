using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podskal.SystemExt.LinqExt
{
    /// <summary>
    /// Represents an equality comparer that can be initialized by simple Func delegate.
    /// </summary>
    /// <typeparam name="T">The type of items to compare.</typeparam>
    public class DelegateEqualityComparer<T> : IEqualityComparer<T>
    {
        private Func<T, T, bool> _Compare;
        private Func<T, int> _Hash;

        public DelegateEqualityComparer(Func<T, T, bool> compare, Func<T, int> hash)
        {
            if (compare == null)
                throw new ArgumentNullException("compare", "The comparer delegate is null");
            if (hash == null)
                throw new ArgumentNullException("hash", "The hasher delegate is null");

            this._Compare = compare;
            this._Hash = hash;
        }

        #region IEqualityComparer<T> Members

        public bool Equals(T x, T y)
        {
            return this._Compare(x, y);
        }

        public int GetHashCode(T obj)
        {
            return this._Hash(obj);
        }

        #endregion
    }
}
