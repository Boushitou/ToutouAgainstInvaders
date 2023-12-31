using Systems.Pooling;
using Systems.EntityState;
using UnityEngine;
using Systems;

namespace Character.Attack
{
    public class PlayerProjectile : MonoBehaviour
    {
        [SerializeField] GameObject _particles;

        private float _speed = 50f;
        private Transform _myTransform;

        private void Awake()
        {
            _myTransform = transform;
        }

        private void Update()
        {
            ProjectileMovement();
            ProjectileOutOfBound();
        }

        public void ProjectileMovement()
        {
            _myTransform.position += Vector3.up * _speed * Time.deltaTime;
        }

        public void ProjectileOutOfBound()
        {
            if (_myTransform.position.y < CameraManager.Instance.GetMinBound().y || _myTransform.position.y > CameraManager.Instance.GetMaxBound().y)
            {
                ObjectPoolManager.ReturnObjectPool(gameObject);
            }
        }

        public void InstantiateParticles()
        {
            ObjectPoolManager.SpawnObject(_particles, _myTransform.position, Quaternion.identity, ObjectPoolManager.PoolType.Particules);
        }
    }
}
