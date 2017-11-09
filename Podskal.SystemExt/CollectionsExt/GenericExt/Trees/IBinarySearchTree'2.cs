using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemExt.CollectionsExt.GenericExt.Trees
{
    /// <summary>
    /// Defines simple binary search tree.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IBinarySearchTree<TKey, TValue> : IDictionary<TKey, TValue>
    {

    }
}
