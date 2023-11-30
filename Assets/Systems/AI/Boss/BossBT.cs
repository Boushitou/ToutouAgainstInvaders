using Character.Attack;
using System.Collections.Generic;
using Systems.EntityState;
using Systems.Pooling;
using UnityEngine;

namespace BehaviourTree
{
    public class BossBT : TreeBT
    {
        [SerializeField] Transform _shootPos;
        private GameObject _player;

        private void OnEnable()
        {
            _player = GameObject.FindWithTag("Player");

            //if (_player != null)
            //    Debug.Log("Found player!");
            //else
            //    Debug.Log("No player found!");
        }

        protected override Node SetupTree()
        {
            Node root = new Sequence(new List<Node>
            {
                new BossPhase1(gameObject),
                new BossPhase2(gameObject),
                new BossPhase3(gameObject, _player, this, _shootPos)
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
                    collision.GetComponent<PlayerProjectile>().InstantiateParticles();
                    ObjectPoolManager.ReturnObjectPool(collision.gameObject);
                }
            }
        }
    }
}