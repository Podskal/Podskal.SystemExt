using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  SystemExt.CollectionsExt.GenericExt.Trees
{
    /// <summary>
    /// Represensts a Mutable binary node that contains some value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class MutableBinaryNode<TValue> : IMutableBinaryNode<MutableBinaryNode<TValue>>,
        IMutableValueNode<TValue>
    {
        #region Constructors

        public MutableBinaryNode()
            : this(default(TValue), null, null)
        {

        }

        public MutableBinaryNode(TValue value) 
            : this(value, null, null)
        {
        }

        public MutableBinaryNode(MutableBinaryNode<TValue> left, MutableBinaryNode<TValue> right) 
            : this(default(TValue), left, right)
        {
        }

        public MutableBinaryNode(TValue value, 
            MutableBinaryNode<TValue> left,
            MutableBinaryNode<TValue> right)
        {
            this.Value = value;
            this.Left = left;
            this.Right = right;
        }

        #endregion


        #region IMutableBinaryNode implementation

        public MutableBinaryNode<TValue> Left
        {
            get;
            set;
        }

        public MutableBinaryNode<TValue> Right
        {
            get;
            set;
        }

        IBinaryNode IBinaryNode.Left
        {
            get { return this.Left; }
        }

        IBinaryNode IBinaryNode.Right
        {
            get { return this.Right; }
        }
        
        public IEnumerable<MutableBinaryNode<TValue>> Children
        {
            get
            {
                if (this.Left != null)
                {
                    yield return this.Left;
                }

                if (this.Right != null)
                {
                    yield return this.Right;
                }
            }
        }

        IEnumerable<INode> INode.Children
        {
            get
            {
                return this.Children;
            }
        }

        #endregion



        #region IMutableValueNode implementation

        public TValue Value
        {
            get;
            set;
        }

        object IValueNode.Value
        {
            get { return this.Value; }
        }

        #endregion


        
        #region Overrides

        public override string ToString()
        {
            return String.Format("Node with value `{0}`",
                this.Value);
        }

        #endregion
    }


    /// <summary>
    /// Contains helper method that simplify creation of typed MutableBinaryNodes
    /// </summary>
    public static class MutableBinaryNode
    {
        public static MutableBinaryNode<TValue> Create<TValue>(TValue value)
        {
            return new MutableBinaryNode<TValue>(value);
        }

        public static MutableBinaryNode<TValue> Create<TValue>(TValue value, MutableBinaryNode<TValue> left, MutableBinaryNode<TValue> right)
        {
            return new MutableBinaryNode<TValue>(value, left, right);
        }

        public static MutableBinaryNode<TValue> Create<TValue>(TValue value, TValue leftValue, TValue rightValue)
        {
            return new MutableBinaryNode<TValue>(value, Create(leftValue), Create(rightValue));
        }
    }
}
