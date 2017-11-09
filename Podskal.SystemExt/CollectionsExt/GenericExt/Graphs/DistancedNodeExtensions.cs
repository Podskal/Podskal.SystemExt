using System;
using System.Collections.Generic;
using SystemExt.LinqExt.ExpressionsExt;

namespace SystemExt.CollectionsExt.GenericExt.Graphs
{
    /// <summary>
    /// Contains extensions methods for distanced graph nodes.
    /// </summary>
    public static class DistancedNodeExtensions
    {
        /// <summary>
        /// Gets a dictionary that contains distances (<typeparamref name="TDistance"/>) 
        /// from the node <paramref name="node"/> to any other nodes it is connected to.
        /// </summary>
        /// <typeparam name="TNode">The type of the node in the tree.</typeparam>
        /// <typeparam name="TDistance"></typeparam>
        /// <param name="node">The node to find distances from.</param>
        /// <param name="zeroDistance">The zero distance (zero value of the <typeparamref name="TDistance"/></param>
        /// <returns>The dictionary that contains distances (<typeparamref name="TDistance"/>) 
        /// from the node <paramref name="node"/> to any other nodes it is connected to.</returns>
        public static IReadOnlyDictionary<TNode, TDistance> GetShortestPathsFrom<TNode, TDistance>(
            this TNode node,
            TDistance zeroDistance = default(TDistance))
        {
            if (node == null)
                throw new ArgumentNullException(paramName: nameof(node));

            return node.GetShortestPathsFrom(
                zeroDistance,
                addDistances: ExpressionExtensions.MakeAddExpression<TDistance>().Compile(),
                distanceComparer: Comparer<TDistance>.Default,
                nodeEqualityComparer: EqualityComparer<TNode>.Default);
        }


        /// <summary>
        /// Gets a dictionary that contains distances (<typeparamref name="TDistance"/>) 
        /// from the node <paramref name="node"/> to any other nodes it is connected to.
        /// </summary>
        /// <typeparam name="TNode">The type of the node in the tree.</typeparam>
        /// <typeparam name="TDistance"></typeparam>
        /// <param name="node">The node to find distances from.</param>
        /// <param name="zeroDistance">The zero distance (zero value of the <typeparamref name="TDistance"/></param>
        /// <returns>The dictionary that contains distances (<typeparamref name="TDistance"/>) 
        /// from the node <paramref name="node"/> to any other nodes it is connected to.</returns>
        public static IReadOnlyDictionary<TNode, TDistance> GetShortestPathsFrom<TNode, TDistance>(
            this TNode node,
            TDistance zeroDistance,
            Func<TDistance, TDistance, TDistance> addDistances,
            IComparer<TDistance> distanceComparer,
            IEqualityComparer<TNode> nodeEqualityComparer)
        {
            if (node == null)
                throw new ArgumentNullException(paramName: nameof(node));
            if (addDistances == null)
                throw new ArgumentNullException(paramName: nameof(addDistances));
            if (distanceComparer == null)
                throw new ArgumentNullException(paramName: nameof(distanceComparer));
            if (nodeEqualityComparer == null)
                throw new ArgumentNullException(paramName: nameof(nodeEqualityComparer));

            return node.GetShortestPathsFromImpl(
                zeroDistance,
                addDistances,
                distanceComparer,
                nodeEqualityComparer);
        }


        /// <summary>
        /// Gets a dictionary that contains distances (<typeparamref name="TDistance"/>) 
        /// from the node <paramref name="node"/> to any other nodes it is connected to.
        /// </summary>
        /// <typeparam name="TNode">The type of the node in the tree.</typeparam>
        /// <typeparam name="TDistance"></typeparam>
        /// <param name="node">The node to find distances from.</param>
        /// <param name="zeroDistance">The zero distance (zero value of the <typeparamref name="TDistance"/></param>
        /// <returns>The dictionary that contains distances (<typeparamref name="TDistance"/>) 
        /// from the node <paramref name="node"/> to any other nodes it is connected to.</returns>
        private static IReadOnlyDictionary<TNode, TDistance> GetShortestPathsFromImpl<TNode, TDistance>(
            this TNode node,
            TDistance zeroDistance,
            Func<TDistance, TDistance, TDistance> addDistances,
            IComparer<TDistance> distanceComparer,
            IEqualityComparer<TNode> nodeEqualityComparer)
        {
            throw new NotImplementedException();
        }
    }
}

