using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  SystemExt.CollectionsExt.GenericExt.Trees
{
    /// <summary>
    /// Defines a simple node that knows its direct children nodes.
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// Gets all children of the node.
        /// </summary>
        /// <remarks>
        /// It is not recommended to return null nodes.
        /// </remarks>
        IEnumerable<INode> Children
        {
            get;
        }
    }
    
    /// <summary>
    /// Defines a simple node that has direct children nodes of the specified type.
    /// </summary>
    /// <typeparam name="TNode">The type of the child node.</typeparam>
    public interface ITypedNode<out TNode> : INode
        where TNode : INode
    {
        /// <summary>
        /// Gets all children of the node.
        /// </summary>
        new IEnumerable<TNode> Children
        {
            get;
        }
    }

    /// <summary>
    /// Defines a node that contains some value.
    /// </summary>
    public interface IValueNode : INode
    {
        /// <summary>
        /// Gets the value containded in the node.
        /// </summary>
        Object Value
        {
            get;
        }
    }


    /// <summary>
    /// Defines a node that has some associated key.
    /// </summary>
    public interface IKeyNode : INode
    {
        /// <summary>
        /// Gets the key containded in the node.
        /// </summary>
        Object Key
        {
            get;
        }
    }


    /// <summary>
    /// Defines a node that knows its parent node.
    /// </summary>
    public interface IParentedNode : INode
    {
        /// <summary>
        /// Gets the parent node for this node.
        /// </summary>
        INode Parent
        {
            get;
        }
    }


    /// <summary>
    /// Defines a node that knows its parent node of the specific type.
    /// </summary>
    /// <typeparam name="TParentNode">The type of the parent node.</typeparam>
    public interface IParentedNode<out TParentNode> : IParentedNode
        where TParentNode : INode
    {
        /// <summary>
        /// Gets the parent node for this node.
        /// </summary>
        new TParentNode Parent
        {
            get;
        }
    }
    
    
    /// <summary>
    /// Defines a node that contains value of the specific type.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained.</typeparam>
    public interface IValueNode<out TValue> : IValueNode
    {
        /// <summary>
        /// Gets the contained value.
        /// </summary>
        new TValue Value
        {
            get;
        }
    }


    /// <summary>
    /// Defines a node that contains key of the specific type.
    /// </summary>
    /// <typeparam name="TKey">The type of the key contained.</typeparam>
    public interface IKeyNode<out TKey> : IKeyNode
    {
        /// <summary>
        /// Gets the contained key.
        /// </summary>
        new TKey Key
        {
            get;
        }
    }
}
