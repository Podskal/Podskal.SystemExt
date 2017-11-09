using System.Collections.Generic;

namespace SystemExt.CollectionsExt.GenericExt.Graphs
{
    /// <summary>
    /// Represents a graph node with typed connected nodes that can be modified.
    /// </summary>
    /// <typeparam name="TNode">The type of the connected nodes.</typeparam>
    public interface IMutableNode<TNode> :
            INode<TNode>
        where TNode : IMutableNode<TNode>
    {
        /// <summary>
        /// Gets the connected nodes.
        /// </summary>
        new ICollection<TNode> Connections
        {
            get;
            set;
        }
    }
}
