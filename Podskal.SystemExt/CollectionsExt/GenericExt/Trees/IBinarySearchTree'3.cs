using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemExt.CollectionsExt.GenericExt.Trees
{
    /// <summary>
    /// Binary search tree that provides Root property to allow generic node manipulations.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TNode">The type of the nodes.</typeparam>
    public interface IBinarySearchTree<TKey, TValue, out TNode> 
            : IBinarySearchTree<TKey, TValue>
        where TNode
            : ISearchNode<TNode, TKey>,
            IBinaryNode<TNode>,
            IValueNode<TValue>
    {
        /// <summary>
        /// Gets the node node for the tree.
        /// </summary>
        TNode Root
        {
            get;
        }
    }
}
