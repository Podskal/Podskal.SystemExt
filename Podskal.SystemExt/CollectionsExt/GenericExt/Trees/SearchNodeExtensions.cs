using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemExt.CollectionsExt.GenericExt.Trees
{
    /// <summary>
    /// Contains extensions methods operating on the SearchNode instances
    /// </summary>
    public static class SearchNodeExtensions
    {
        #region Search
        
        /// <summary>
        /// Finds the node by the associated key.
        /// </summary>
        /// <typeparam name="TNode">The type of the nodes to search .</typeparam>
        /// <typeparam name="TKey">The type of the key to find.</typeparam>
        /// <param name="node">The starting node.</param>
        /// <param name="key">The key to find.</param>
        /// <returns>The first node that contains the given key.</returns>
        public static TNode FindByKey<TNode, TKey>(this TNode node, TKey key)
            where TNode : class,
                ISearchNode<TNode, TKey>
        {
            if (node == null)
                throw new ArgumentNullException("node");

            TNode current = node;

            while (current != null)
            {
                if (current.IsWithKey(key))
                    return current;

                current = current.DirectChildForKey(key);
            }

            return null;
        }


        /// <summary>
        /// Gets the lowest common ancestor for nodes with keys keyA and keyB in the tree with root node.
        /// </summary>
        /// <typeparam name="TNode">The type of the tree nodes.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="node">The root of the tree.</param>
        /// <param name="keyA">The key of the first node.</param>
        /// <param name="keyB">The key of the second node.</param>
        /// <returns>
        /// The lowest common ancestor for nodes with keys keyA and keyB in the tree with root node. 
        /// Or null if no possible ancestor found.
        /// </returns>
        public static TNode LowestCommonAncestor<TNode, TKey>(this TNode node, TKey keyA, TKey keyB)
            where TNode : class,
                ISearchNode<TNode, TKey>
        {
            if (node == null)
                throw new ArgumentNullException("node");

            return node.LowestCommonAncestorImpl(keyA, keyB);
        }


        private static TNode LowestCommonAncestorImpl<TNode, TKey>(this TNode node, TKey keyA, TKey keyB)
            where TNode : class,
                ISearchNode<TNode, TKey>
        {
            TNode prev;
            TNode current = null;
            TNode nodeA = node;
            TNode nodeB;

            do
            {
                prev = current;
                current = nodeA;

                if ((current.IsWithKey(keyA)) ||
                    (current.IsWithKey(keyB)))
                {
                    return prev;
                }

                nodeA = current.DirectChildForKey(keyA);
                nodeB = current.DirectChildForKey(keyB);

                if (nodeA == null)
                    throw new ArgumentException("There is no node with key keyA", "keyA");
                if (nodeB == null)
                    throw new ArgumentException("There is no node with key keyB", "keyB");

            }
            while (nodeA == nodeB);

            return current;
        }

        #endregion
    }
}
