using System;

namespace SystemExt.CollectionsExt.GenericExt.Graphs
{
    /// <summary>
    /// Contains factory methods for <see cref="NodeWithMutableDistance{TNode, TDistance}"/> instances.
    /// </summary>
    public static class NodeWithMutableDistance
    {
        #region Methods

        /// <summary>
        /// Makes a new <see cref="NodeWithMutableDistance{TNode, TDistance}"/> from the given parameters.
        /// </summary>
        public static NodeWithMutableDistance<TNode, TDistance> Make<TNode, TDistance>(
            TNode destination,
            TDistance distance = default(TDistance))
            where TNode : INode<TNode>
        {
            return new NodeWithMutableDistance<TNode, TDistance>(
                destination: destination,
                distance: distance);
        }

        #endregion
    }



    /// <summary>
    /// Represents a node with associated distance to it from some other node.
    /// </summary>
    /// <typeparam name="TNode">The type of the node.</typeparam>
    /// <typeparam name="TDistance">The type of the distance to the node.</typeparam>
    public class NodeWithMutableDistance<TNode, TDistance> :
            INodeWithMutableDistance<TNode, TDistance>
        where TNode : INode<TNode>
    {
        #region Fields 

        private readonly TNode _destination;

        #endregion


        #region Constructors

        /// <summary>
        /// Initializes a new <see cref="NodeWithMutableDistance{TNode, TDistance}"/> that connects to the specified <paramref name="destination"/>
        /// through the given optional <paramref name="distance"/>.
        /// </summary>
        public NodeWithMutableDistance(
            TNode destination,
            TDistance distance)
        {
            if (destination == null)
                throw new ArgumentNullException(paramName: "node");

            this._destination = destination;
            this.Distance = distance;
        }

        #endregion


        #region IGraphNodeWithMutableDistance implementation

        /// <summary>
        /// Gets or sets the distance to the node.
        /// </summary>
        public TDistance Distance
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the node.
        /// </summary>
        public TNode Destination
        {
            get
            {
                return this._destination;
            }
        }

        #endregion
    }
}
