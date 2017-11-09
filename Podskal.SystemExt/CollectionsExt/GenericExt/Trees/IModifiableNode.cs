using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  SystemExt.CollectionsExt.GenericExt.Trees
{
    /// <summary>
    /// Defines a node whose parent node can be changed.
    /// </summary>
    /// <typeparam name="TParentNode">The type of the parent node.</typeparam>
    public interface IMutableParentedNode<TParentNode> : IParentedNode<TParentNode>
        where TParentNode : INode
    {
        /// <summary>
        /// Gets or sets the parent node.
        /// </summary>
        new TParentNode Parent
        {
            get;
            set;
        }
    }


    /// <summary>
    /// Defines a node whose contained value can be changed.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface IMutableValueNode<TValue> : IValueNode<TValue>
    {
        /// <summary>
        /// Gets or sets the contained value.
        /// </summary>
        new TValue Value
        {
            get;
            set;
        }
    }
}
