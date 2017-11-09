using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemExt.CollectionsExt.GenericExt.Trees
{
    public class BinarySearchNode<TKey, TValue> : ISearchNode<BinarySearchNode<TKey, TValue>, TKey>,
        IMutableBinaryNode<BinarySearchNode<TKey, TValue>>,
        IKeyNode<TKey>,
        IMutableValueNode<TValue>,
        IComparable<BinarySearchNode<TKey, TValue>>
    {
        #region Fields

        protected BinarySearchNode<TKey, TValue> m_Left;
        protected BinarySearchNode<TKey, TValue> m_Right;
        protected IComparer<TKey> m_KeyComparer;

        #endregion



        #region Constuctors

        public BinarySearchNode(TKey key, TValue value)
            : this(key, value, null, null, Comparer<TKey>.Default)
        {

        }


        public BinarySearchNode(TKey key, TValue value, IComparer<TKey> keyComparer)
            : this(key, value, null,  null, keyComparer)
        {

        }


        public BinarySearchNode(TKey key, 
            TValue value,
            BinarySearchNode<TKey, TValue> left,
            BinarySearchNode<TKey, TValue> right) 
            :
            this(key, value, left, right, Comparer<TKey>.Default)
        {

        }


        public BinarySearchNode(TKey key,
            TValue value,
            BinarySearchNode<TKey, TValue> left,
            BinarySearchNode<TKey, TValue> right,
            IComparer<TKey> keyComparer)
        {
            this.Key = key;
            this.Value = value;
            this.m_KeyComparer = keyComparer;

            this.Left = left;
            this.Right = right;
        }
         

        #endregion


        #region ISearchNode implementation

        public bool IsWithKey(TKey key)
        {
            return (this.m_KeyComparer.Compare(key, this.Key) == 0);
        }

        public BinarySearchNode<TKey, TValue> DirectChildForKey(TKey key)
        {
            var cmp = this.m_KeyComparer.Compare(key, this.Key);

            if (cmp < 0)
            {
                return this.Left;
            }

            if (cmp == 0)
            {
                return this;
            }

            return this.Right;
        }

        public IEnumerable<BinarySearchNode<TKey, TValue>> Children
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
            get { return this.Children; }
        }

        #endregion


        #region IMutableBinaryNode implementation


        public Boolean CanBeLeft(BinarySearchNode<TKey, TValue> left)
        {
            if (left == null)
                return true;

            if (this.m_KeyComparer.Compare(left.Key, this.Key) < 0)
                return true;

            return false;
        }

        public Boolean CanBeRight(BinarySearchNode<TKey, TValue> right)
        {
            if (right == null)
                return true;

            if (this.m_KeyComparer.Compare(right.Key, this.Key) > 0)
                return true;

            return false;
        }


        public BinarySearchNode<TKey, TValue> Left
        {
            get
            {
                return this.m_Left;
            }
            set
            {
                if (!this.CanBeLeft(value))
                    throw new ArgumentException("Such node cannot be used as left node for the current node.", "value");

                this.m_Left = value;
            }
        }

        public BinarySearchNode<TKey, TValue> Right
        {
            get
            {
                return this.m_Right;
            }
            set
            {
                if (!this.CanBeRight(value))
                    throw new ArgumentException("Such node cannot be used as right node for the current node.", "value");

                this.m_Right = value;
            }
        }

        IBinaryNode IBinaryNode.Left
        {
            get { return this.Left; }
        }

        IBinaryNode IBinaryNode.Right
        {
            get { return this.Right; }
        }

        #endregion



        #region IKeyNode implementation

        public TKey Key
        {
            get;
            private set;
        }

        object IKeyNode.Key
        {
            get { return this.Key; }
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


        #region IComparable implementation

        public int CompareTo(BinarySearchNode<TKey, TValue> other)
        {
            throw new NotImplementedException();
        }

        #endregion



        #region Overrides

        public override string ToString()
        {
            return String.Format("<{0}, {1}>",
                this.Key,
                this.Value);
        }

        #endregion
    }


    /// <summary>
    /// Contains helper create methods to avoid generic parameter specification for BinarySearchNode constructors.
    /// </summary>
    public static class BinarySearchNode
    {        
        #region Static members

        public static BinarySearchNode<TKey, TValue> Create<TKey, TValue>(TKey key, 
            TValue value,
            BinarySearchNode<TKey, TValue> left,
            BinarySearchNode<TKey, TValue> right)
        {
            return new BinarySearchNode<TKey, TValue>(key, value, left, right);
        }


        public static BinarySearchNode<TKey, TKey> Create<TKey>(TKey key,
            BinarySearchNode<TKey, TKey> left,
            BinarySearchNode<TKey, TKey> right)
        {
            return new BinarySearchNode<TKey, TKey>(key, key, left, right);
        }


        public static BinarySearchNode<TKey, TKey> Create<TKey>(TKey key)
        {
            return new BinarySearchNode<TKey, TKey>(key, key, null, null);
        }


        public static BinarySearchNode<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value)
        {
            return new BinarySearchNode<TKey, TValue>(key, value, null, null);
        }

        #endregion
    }

}


