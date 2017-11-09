using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemExt.CollectionsExt.GenericExt.Trees
{
    /// <summary>
    /// Defines a binary node whose child nodes can be changed.
    /// </summary>
    /// <typeparam name="TNode">The type of the child nodes.</typeparam>
    public interface IMutableBinaryNode<TNode> : IBinaryNode<TNode>
        where TNode : IMutableBinaryNode<TNode>
    {
        /// <summary>
        /// Gets or sets the left child.
        /// </summary>
        new TNode Left
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the right child.
        /// </summary>
        new TNode Right
        {
            get;
            set;
        }
    }
}
