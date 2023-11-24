using System.Collections.Generic;

namespace BehaviourTree
{
    public class BossBT : TreeBT
    {
        //private int _currentPhase = 1;

        protected override Node SetupTree()
        {
            Node root = new Sequence(new List<Node>
            {
                new BossPhase1(gameObject)
                //phase2
                //phase3
            });

            return root;
        }
    }
}