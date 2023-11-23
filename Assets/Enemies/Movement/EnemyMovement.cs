using UnityEngine;
using Systems.Pooling;
using Systems.Spawn;

namespace Enemies.Movement
{
    public abstract class EnemyMovement : MonoBehaviour
    {
        protected Transform _myTransform;
        protected float _speed = 0f;

        private Camera _cam;
        private Vector2 _minBound;
        private float _offset = 5f;

        private void Awake()
        {
            _myTransform = transform;
            _cam = Camera.main;
            _minBound = _cam.ViewportToWorldPoint(new Vector2(0, 0));
        }

        private void Update()
        {
            DestroyEnemy();
            Movement();
        }

        public abstract void Movement();

        public void DestroyEnemy()
        {
            if (_myTransform.position.y < _minBound.y - _offset)
            {
                ObjectPoolManager.ReturnObjectPool(gameObject);
                SpawnerManager.Instance.RemoveEnemy();
            }
        }
    }
}
