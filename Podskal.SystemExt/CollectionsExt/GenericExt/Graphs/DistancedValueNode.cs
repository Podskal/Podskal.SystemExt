using System;
using System.Collections.Generic;
using System.Linq;

namespace SystemExt.CollectionsExt.GenericExt.Graphs
{
    /// <summary>
    /// Represents a standard fully mutable graph node that contains some value and that knows distance to its connected neighbours.
    /// </summary>
    /// <typeparam name="TDistance">The type of the distance.</typeparam>
    /// <typeparam name="TValue">The type of the contained value.</typeparam>
    public class DistancedValueNode<TDistance, TValue> :
        IMutableDistancedNode<DistancedValueNode<TDistance, TValue>, TDistance, NodeWithMutableDistance<DistancedValueNode<TDistance, TValue>, TDistance>>,
        IMutableValueNode<TValue>
    {
        #region Fields

        private ICollection<NodeWithMutableDistance<DistancedValueNode<TDistance, TValue>, TDistance>> _connections;

        #endregion


        #region Constructors

        /// <summary>
        /// Initializes a new instance of DistancedValueGraphNode to the given value and empty connections.
        /// </summary>
        /// <param name="value">The value to be contained in the node.</param>
        public DistancedValueNode(TValue value)
        {
            this.Value = value;
            this._connections = new List<NodeWithMutableDistance<DistancedValueNode<TDistance, TValue>, TDistance>>();
        }

        #endregion


        #region IDistancedGraphNode implementation

        /// <summary>
        /// Gets or sets the connected nodes and the respective distances.
        /// </summary>
        public ICollection<NodeWithMutableDistance<DistancedValueNode<TDistance, TValue>, TDistance>> Connections
        {
            get
            {
                return this._connections;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                this._connections = value;
            }
        }


        IEnumerable<INode> INode.Connections
        {
            get
            {
                return this
                    .Connections
                    .Select(nodeDistance =>
                        nodeDistance.Destination);
            }
        }


        IEnumerable<DistancedValueNode<TDistance, TValue>> INode<DistancedValueNode<TDistance, TValue>>.Connections
        {
            get
            {
                return this
                    .Connections
                    .Select(nodeDistance =>
                        nodeDistance.Destination);
            }
        }


        IEnumerable<NodeWithMutableDistance<DistancedValueNode<TDistance, TValue>, TDistance>> IDistancedNode<DistancedValueNode<TDistance, TValue>, TDistance, NodeWithMutableDistance<DistancedValueNode<TDistance, TValue>, TDistance>>.Connections
        {
            get
            {
                return this
                    .Connections;
            }
        }


        IEnumerable<INodeWithDistance<DistancedValueNode<TDistance, TValue>, TDistance>> IDistancedNode<DistancedValueNode<TDistance, TValue>, TDistance>.Connections
        {
            get
            {
                return this
                    .Connections;
            }
        }

        #endregion


        #region IMutableValueGraphNode implementation

        /// <summary>
        /// Gets the value contained in the node.
        /// </summary>
        public TValue Value
        {
            get;
            set;
        }

        #endregion
    }
}
