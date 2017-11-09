namespace SystemExt.CollectionsExt.GenericExt.Graphs
{
    /// <summary>
    /// Represents a graph node that can additionally contain some readonly value.
    /// </summary>
    /// <typeparam name="TValue">The type of the contained value.</typeparam>
    public interface IValueNode<out TValue> : INode
    {
        /// <summary>
        /// Gets the contained value.
        /// </summary>
        TValue Value
        {
            get;
        }
    }


    /// <summary>
    /// Represents a graph node that can additionally contain some mutable value.
    /// </summary>
    /// <typeparam name="TValue">The type of the contained value.</typeparam>
    public interface IMutableValueNode<TValue> : IValueNode<TValue>
    {
        /// <summary>
        /// Gets the contained value.
        /// </summary>
        new TValue Value
        {
            get;
            set;
        }
    }
}
