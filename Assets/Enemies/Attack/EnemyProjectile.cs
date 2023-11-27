using Systems;
using Systems.EntityState;
using Systems.Pooling;
using UnityEngine;

namespace Enemies.Attack
{
    public class EnemyProjectile : MonoBehaviour
    {
        private float _speed = 8f;
        private int _damage = 15;
        private Transform _myTransform;
        private Vector3 _direction = Vector2.down;

        private void Awake()
        {
            _myTransform = transform;
        }

        // Update is called once per frame
        void Update()
        {
            ProjectileMovement();
            ProjectileOutOfBound();
        }

        public void ProjectileMovement()
        {
            _myTransform.Translate(_direction * _speed * Time.deltaTime);
        }

        public void ProjectileOutOfBound()
        {
            if (_myTransform.position.y < CameraManager.Instance.GetMinBound().y || _myTransform.position.y > CameraManager.Instance.GetMaxBound().y ||
                _myTransform.position.x < CameraManager.Instance.GetMinBound().x || _myTransform.position.x > CameraManager.Instance.GetMaxBound().x)
            {
                ObjectPoolManager.ReturnObjectPool(gameObject);
            }
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }

        public int GetDamagge()
        {
            return _damage;
        }
    }
}