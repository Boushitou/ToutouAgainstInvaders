using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;


/* Represent an element in the tree, can access both children and parents */
namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    public class Node
    {
        protected NodeState state;

        /* The Node is public so we can share data by backtracking in the branch */
        public Node parent;

        /* Children will be assigned in the constructor of the class */
        protected List<Node> children = new List<Node>();

        public Node() //Empty by default
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
                _Attach(child);
        }

        /* Create an edge between a node and its new child */
        private void _Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        /* The shared data will be stored in the dictionary */
        private Dictionary<string, object>_dataContext =
            new Dictionary<string, object>();

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;

            Node node = parent;

            while (node!= null)
            {
                /* Work up the branch until it finds the key or reached the root of the tree*/
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        /* Remove the key from the dictionary after it has been found */
        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while (node!=null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }
}