
namespace SystemExt.CollectionsExt.GenericExt.Graphs
{
    /// <summary>
    /// Represents a node with associated distance to it from some other node.
    /// </summary>
    /// <typeparam name="TNode">The type of the node.</typeparam>
    /// <typeparam name="TDistance">The type of the distance to the node.</typeparam>
    public interface INodeWithDistance<TNode, TDistance>
        where TNode : INode<TNode>
    {
        /// <summary>
        /// Gets the node.
        /// </summary>
        TNode Destination
        {
            get;
        }

        /// <summary>
        /// Gets the distance to the node.
        /// </summary>
        TDistance Distance
        {
            get;
        }
    }


    /// <summary>
    /// Represents a node with associated distance to it from some other node.
    /// </summary>
    /// <typeparam name="TNode">The type of the node.</typeparam>
    /// <typeparam name="TDistance">The type of the distance to the node.</typeparam>
    public interface INodeWithMutableDistance<TNode, TDistance> :
            INodeWithDistance<TNode, TDistance>
        where TNode : INode<TNode>
    {
        /// <summary>
        /// Gets or sets the distance to the node.
        /// </summary>
        new TDistance Distance
        {
            get;
            set;
        }
    }
}
