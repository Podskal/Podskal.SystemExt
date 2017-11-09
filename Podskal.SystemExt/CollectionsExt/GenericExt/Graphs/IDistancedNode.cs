using System.Collections.Generic;

namespace SystemExt.CollectionsExt.GenericExt.Graphs
{
    /// <summary>
    /// Represents a graph node with connections to other nodes, that are at some specific distance from this node.
    /// </summary>
    /// <typeparam name="TNode">The type of the node.</typeparam>
    /// <typeparam name="TDistance">The type of the distance.</typeparam>
    public interface IDistancedNode<TNode, TDistance> :
            INode<TNode>
        where TNode : IDistancedNode<TNode, TDistance>
    {
        /// <summary>
        /// Gets the connections (nodes + respective distances).
        /// </summary>
        new IEnumerable<INodeWithDistance<TNode, TDistance>> Connections
        {
            get;
        }
    }


    /// <summary>
    /// Represents a graph node with connections to other nodes, that are at some specific distance from this node.
    /// </summary>
    /// <typeparam name="TNode">The type of the node.</typeparam>
    /// <typeparam name="TDistance">The type of the distance.</typeparam>
    public interface IDistancedNode<TNode, TDistance, TNodeWithDistance> :
            IDistancedNode<TNode, TDistance>
        where TNode : IDistancedNode<TNode, TDistance, TNodeWithDistance>
        where TNodeWithDistance: INodeWithDistance<TNode, TDistance>
    {
        /// <summary>
        /// Gets the connections (nodes + respective distances).
        /// </summary>
        new IEnumerable<TNodeWithDistance> Connections
        {
            get;
        }
    }


    /// <summary>
    /// Represents a graph node with connections to other nodes, that are at some specific distance from this node.
    /// </summary>
    /// <typeparam name="TNode">The type of the node.</typeparam>
    /// <typeparam name="TDistance">The type of the distance.</typeparam>
    public interface IMutableDistancedNode<TNode, TDistance, TNodeWithDistance> : 
            IDistancedNode<TNode, TDistance, TNodeWithDistance>
        where TNode : IMutableDistancedNode<TNode, TDistance, TNodeWithDistance>
        where TNodeWithDistance : INodeWithDistance<TNode, TDistance>
    {
        /// <summary>
        /// Gets the connections (nodes + respective distances).
        /// </summary>
        new ICollection<TNodeWithDistance> Connections
        {
            get;
            set;
        }
    }
}
