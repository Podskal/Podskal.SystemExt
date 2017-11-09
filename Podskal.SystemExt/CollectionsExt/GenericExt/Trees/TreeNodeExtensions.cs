using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  SystemExt.CollectionsExt.GenericExt.Trees
{
    /// <summary>
    /// Contains extension methods that operate on different INode derived interfaces.
    /// </summary>
    public static class TreeNodeExtensions
    {
        #region Properties

        /// <summary>
        /// Gets the height of the tree from the given node to its lowest child.
        /// </summary>
        /// <param name="node">The node to start counting from.</param>
        /// <returns>The height of the tree from the given node to its lowest child.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static Int32 Height(this INode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            return node.Height(1);
        }
        
        /// <summary>
        /// Gets the height of the tree from the given node to its lowest child. 
        /// Considers the node node to be at the initial depth of beforeHeight
        /// </summary>
        /// <param name="node">The node to start counting from.</param>
        /// <returns>The height of the tree from the given node to its lowest child.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static Int32 Height(this INode node, Int32 beforeHeight)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            return node
                .Children
                .Where(child => 
                    child != null)
                .Select(child =>
                    child.Height(beforeHeight + 1))
                .Concat(new Int32[] { beforeHeight })
                .Max();
        }

        #endregion



        #region Traversals


        /// <summary>
        /// Traverses the tree starting from the given node using width first approach.
        /// </summary>
        /// <typeparam name="TNode">The type of the node</typeparam>
        /// <param name="node">The node to start traversal from</param>
        /// <returns>The nodes in the tree enumerated with width first appoach.</returns>
        public static IEnumerable<TNode> TraverseWidth<TNode>(this TNode node)
            where TNode : ITypedNode<TNode>
        {
            if (node == null)
                throw new ArgumentNullException("node");

            return node
                .TraverseWidth_GroupByDepth()
                .SelectMany(levelNodes =>
                    levelNodes.Value);
        }


        /// <summary>
        /// Traverses tree using width first algorithmes and returns items in batches with corresponding depth.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<Int32, IEnumerable<TNode>>> TraverseWidth_GroupByDepth<TNode>(this TNode node)
            where TNode : ITypedNode<TNode>
        {
            if (node == null)
                throw new ArgumentNullException("node");

            var nextLevelNodes = new Queue<TNode>();
            nextLevelNodes.Enqueue(node);

            var currentLevelNodes = new Queue<TNode>();

            var currentDepth = 0;            

            do
            {
                // Swap current level with next;
                var temp = nextLevelNodes;
                nextLevelNodes = currentLevelNodes;
                currentLevelNodes = temp;
                
                // Enumerate all appropriate current level nodes to enqueue their children
                foreach (var curNode in currentLevelNodes)
                {
                    if (curNode == null)
                        continue;

                    // Enqueue children for current node
                    foreach (var curNodeChild in curNode.Children)
	                {
                        nextLevelNodes.Enqueue(curNodeChild);
	                }
                }

                // Yield with explicitly undeferred selection of current level nodes.
                // It is required to avoid problems if not all levels of nodes are enumerated
                // in their original ordering - we use the same queue each time.
                yield return new KeyValuePair<Int32, IEnumerable<TNode>>(
                    currentDepth,
                    currentLevelNodes.ToArray());

                currentLevelNodes.Clear();
                currentDepth++;
            }
            while (nextLevelNodes.Count > 0);
        }
        

        /// <summary>
        /// Traverses the tree defined by the given node node with the preorder traversal.
        /// It returns the leftmost node on the current level, then begins to process its children nodes.
        /// If there are no more nodes on the current level, it returns one level up and continues processing of
        /// the untouched nodes.
        /// </summary>
        /// <typeparam name="TNode">The type of the node.</typeparam>
        /// <param name="node">The node to start traversal from.</param>
        /// <returns>The preorder traversal sequence.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static IEnumerable<TNode> TraversePreorder<TNode>(this TNode node)
            where TNode : ITypedNode<TNode>
        {
            if (node == null)
                throw new ArgumentNullException("node");

            return node.TraversePreorderImpl();
        }


        private static IEnumerable<TNode> TraversePreorderImpl<TNode>(this TNode node)
            where TNode : ITypedNode<TNode>
        {
            yield return node;

            foreach (var child in node.Children)
            {
                if (child == null)
                    continue;

                foreach (var traversalItem in child.TraversePreorderImpl())
                {
                    yield return traversalItem;
                }
            }
        }


        /// <summary>
        /// Traverses the tree defined by the given node node with the preorder traversal.
        /// It returns the leftmost node on the current level, then begins to process its children nodes.
        /// If there are no more nodes on the current level, it returns one level up and continues processing of
        /// the untouched nodes.
        /// It is the non-recursive version.
        /// </summary>
        /// <typeparam name="TNode">The type of the node.</typeparam>
        /// <param name="node">The node to start traversal from.</param>
        /// <returns>The preorder traversal sequence.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static IEnumerable<TNode> TraversePreorderNonRecursive<TNode>(this TNode node)
            where TNode : ITypedNode<TNode>
        {
            if (node == null)
                throw new ArgumentNullException("node");

            Stack<IEnumerator<TNode>> nodeEnumerators = new Stack<IEnumerator<TNode>>();

            try
            {
                // Push node Children enumerator
                nodeEnumerators.Push(node.Children.GetEnumerator());

                // Yield node
                yield return node;

                IEnumerator<TNode> currentEnumerator = null;
                TNode currentNode;

                // Process till enumerators stack is  empty
                while (nodeEnumerators.Count != 0)
                {
                    // Peek current enumerator
                    currentEnumerator = nodeEnumerators.Peek();

                    if (!currentEnumerator.MoveNext())
                    {
                        // Pop finished enumerator and dispose it
                        nodeEnumerators.Pop();
                        currentEnumerator.Dispose();

                        continue;
                    }

                    currentNode = currentEnumerator.Current;

                    if (currentNode == null)
                        continue;

                    yield return currentNode;

                    // Add new enumerator to the stack
                    nodeEnumerators.Push(currentNode.Children.GetEnumerator());
                }
            }
            finally
            {
                // Ensure that all enumerator are disposed of.
                foreach (var iEnumerator in nodeEnumerators)
                {
                    iEnumerator.Dispose();
                }
            }
        }
        
        #endregion



        #region Search

        /// <summary>
        /// Searches for the ValueNode that contains the value equal to the valueToFind.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="node"></param>
        /// <param name="valueToFind"></param>
        /// <param name="areEqual"></param>
        /// <returns></returns>
        public static TNode FindByValue<TNode, TValue>(this TNode node, TValue valueToFind, Func<TValue, TValue, Boolean> areEqual)
            where TNode : class,
                IValueNode<TValue>, 
                ITypedNode<TNode>
        {
            if (node == null)
                throw new ArgumentNullException("node");
            if (areEqual == null)
                throw new ArgumentNullException("areEqual");

            return node.FindByValueImpl(valueToFind, areEqual);
        }


        private static TNode FindByValueImpl<TNode, TValue>(this TNode node, TValue valueToFind, Func<TValue, TValue, Boolean> areEqual)
            where TNode : class,
                IValueNode<TValue>, 
                ITypedNode<TNode>
        {
            return node
                .TraversePreorder()
                .FirstOrDefault(aNode =>
                    areEqual(valueToFind, aNode.Value));
        }

        #endregion
    }
}
