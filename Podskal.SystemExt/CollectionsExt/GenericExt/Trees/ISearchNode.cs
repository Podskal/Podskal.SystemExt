using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemExt.CollectionsExt.GenericExt.Trees
{
    /// <summary>
    /// Defines a node that can be used in search trees.
    /// </summary>
    /// <typeparam name="TKey">The type of the key used for search.</typeparam>
    /// <typeparam name="TNode">The type of the nodes that can be seached.</typeparam>
    public interface ISearchNode<out TNode, in TKey> : ITypedNode<TNode>
        where TNode : ISearchNode<TNode, TKey>
    {
        Boolean IsWithKey(TKey key);

        /// <summary>
        /// Gets the direct child node that can contain the given key.
        /// Does not return itself if it contains the given key itself.
        /// </summary>
        /// <param name="key">The key to find the child</param>
        /// <returns>
        /// The direct child node that can contain the given key.
        /// Or null if there are no appropriate nodes.
        /// </returns>
        TNode DirectChildForKey(TKey key);
    }


}
