using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemExt.CollectionsExt.GenericExt.Trees
{    /// <summary>
    /// Defines a binary node.
    /// </summary>
    public interface IBinaryNode : INode
    {
        /// <summary>
        /// Gets the left child.
        /// </summary>
        IBinaryNode Left
        {
            get;
        }

        /// <summary>
        /// Gets the right child.
        /// </summary>
        IBinaryNode Right
        {
            get;
        }
    }


    /// <summary>
    /// Defines a binary node that has children nodes of the specified type.
    /// </summary>
    /// <typeparam name="TNode"></typeparam>
    public interface IBinaryNode<out TNode> : IBinaryNode,
            ITypedNode<TNode>
        where TNode : IBinaryNode
    {
        /// <summary>
        /// Gets the left child.
        /// </summary>
        new TNode Left
        {
            get;
        }

        /// <summary>
        /// Gets the right child.
        /// </summary>
        new TNode Right
        {
            get;
        }
    }
}
