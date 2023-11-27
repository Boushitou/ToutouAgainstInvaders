using System.Collections.Generic;
using Systems.EntityState;
using Systems.Pooling;
using UnityEngine;

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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null)
            {
                if (collision.gameObject.CompareTag("PlayerBullet"))
                {
                    GetComponentInParent<Health>().TakeDamage(1);
                    ObjectPoolManager.ReturnObjectPool(collision.gameObject);

                }
            }
        }
    }
}