using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class TreeBT : MonoBehaviour
    {
        private Node _root = null;
        // Start is called before the first frame update
        protected virtual void Start()
        {
            /* Build the Behaviour Tree */
            _root = SetupTree();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (_root != null)
            {
                /* Continuously evaluate the BT*/
                _root.Evaluate();
            }
        }
        protected abstract Node SetupTree();
    }
}
