using UnityEngine;
using Systems.Pooling;

namespace Character.Attack
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _projectileSpawn;

        private float _atkSpeed = 0.3f;
        private float _coolDown = 0;
        private bool _isShooting;

        private void Update()
        {
            ShootProjectile();
        }

        public void ShootProjectile()
        {
            if (_isShooting)
            {
                if (Time.time > _coolDown)
                {
                    ProjectilePoolManager.SpawnObject(_projectilePrefab, _projectileSpawn.position, Quaternion.identity);
                    _coolDown = Time.time + _atkSpeed;
                }
            }
        }

        public void SetIsShooting()
        {
            _isShooting = !_isShooting;
        }

        public float GetAtkSpeed()
        {
            return _atkSpeed;
        }
    }
}
