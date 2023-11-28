using System.Collections.Generic;
using Systems.EntityState;
using Systems.Pooling;
using UnityEngine;

namespace BehaviourTree
{
    public class BossBT : TreeBT
    {
        protected override Node SetupTree()
        {
            Node root = new Sequence(new List<Node>
            {
                new BossPhase1(gameObject),
                new BossPhase2(gameObject)
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