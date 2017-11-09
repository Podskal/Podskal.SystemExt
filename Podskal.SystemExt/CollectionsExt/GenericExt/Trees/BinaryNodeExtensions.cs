using System;
using System.Collections.Generic;

namespace SystemExt.CollectionsExt.GenericExt.Trees
{
    /// <summary>
    /// Contains extension methods to operate on binary nodes.
    /// </summary>
    public static class BinaryNodeExtensions
    {
        #region Traversals

        /// <summary>
        /// Traverses the tree defined by the given node node in order.
        /// Assuming that tree is ordered binary tree it will return its nodes 
        /// in order respective to the key values.
        /// </summary>
        /// <typeparam name="TNode">The type of the node.</typeparam>
        /// <param name="node">The root of the tree to start traversal with.</param>
        /// <returns>The inorder traversal of the tree.</returns>
        /// <exception cref="System.NullArgumentException"></exception>
        public static IEnumerable<TNode> TraverseInOrder<TNode>(this TNode node)
            where TNode : IBinaryNode<TNode>
        {
            if (node == null)
                throw new ArgumentNullException("node");

            return node.TraverseInOrderImpl();
        }


        /// <summary>
        /// No check implementation.
        /// Traverses the tree defined by the given node <paramref name="node"/> in order.
        /// Assuming that tree is ordered binary tree it will return its nodes 
        /// in order respective to the key values.
        /// </summary>
        /// <typeparam name="TNode">The type of the node.</typeparam>
        /// <param name="node">The root of the tree to start traversal with.</param>
        /// <returns>The inorder traversal of the tree.</returns>
        private static IEnumerable<TNode> TraverseInOrderImpl<TNode>(this TNode node)
            where TNode : IBinaryNode<TNode>
        {
            if (node.Left != null)
            {
                foreach (var child in node.Left.TraverseInOrderImpl())
                {
                    yield return child;
                }
            }

            yield return node;

            if (node.Right != null)
            {
                foreach (var child in node.Right.TraverseInOrderImpl())
                {
                    yield return child;
                }
            }
        }

        #endregion


        #region Balancing

        /// <summary>
        /// Balances by one level a tree defined by the given node by moving nodes from left subtree to right.
        /// Preserves the ordering of 
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static TNode BalanceSingleLevel_LeftToRight<TNode>(this TNode node)
            where TNode : IMutableBinaryNode<TNode>
        {
            if (node == null)
                throw new ArgumentNullException("node");

            if (node.Left == null)
            {
                // input node is a leaf node - no need to balance
                if (node.Right == null)
                    return node;

                // No left node, but there is right - invalid operation
                throw new InvalidOperationException("Right node is not null, while left is");
            }

            TNode originalLeft = node.Left;            
            
            node.Left = originalLeft.Right;
            originalLeft.Right = node;            

            return originalLeft;
        }

        #endregion
    }
}
