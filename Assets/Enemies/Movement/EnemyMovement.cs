using UnityEngine;
using Systems.Pooling;
using Systems.Spawn;
using Systems;
using Enemies.Attack;
using Systems.EntityState;

namespace Enemies.Movement
{
    public abstract class EnemyMovement : MonoBehaviour
    {
        protected Transform _myTransform;
        protected float _speed = 0f;

        private float _offset = 5f;

        private void Awake()
        {
            _myTransform = transform;
        }

        private void Update()
        {
            DestroyEnemy();
            Movement();
        }

        public abstract void Movement();

        public void DestroyEnemy()
        {
            if (_myTransform.position.y < CameraManager.Instance.GetMinBound().y - _offset)
            {
                ObjectPoolManager.ReturnObjectPool(gameObject);
                SpawnerManager.Instance.RemoveEnemy();
            }
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
