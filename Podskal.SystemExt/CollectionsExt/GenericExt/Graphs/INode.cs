using System.Collections.Generic;

namespace SystemExt.CollectionsExt.GenericExt.Graphs
{
    /// <summary>
    /// Represents a graph node that can contain no value, but can provide nodes that are connected to it.
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// Gets the connected nodes
        /// </summary>
        IEnumerable<INode> Connections
        {
            get;
        }
    }


    /// <summary>
    /// Represents a graph node with typed connected nodes.
    /// </summary>
    /// <typeparam name="TNode">The type of the connected nodes.</typeparam>
    public interface INode<out TNode> : INode
        where TNode : INode<TNode>
    {
        /// <summary>
        /// Gets the connected nodes.
        /// </summary>
        new IEnumerable<TNode> Connections
        {
            get;
        }
    }
}


