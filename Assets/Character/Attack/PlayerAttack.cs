using UnityEngine;
using Systems.Pooling;
using Sound;

namespace Character.Attack
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _projectileSpawn;

        private float _atkSpeed = 0.1f;
        private float _coolDown = 0;
        private bool _isShooting;

        private float _soundPlayTimer = 0.2f;
        private float _soundCooldown = 0;

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
                    ObjectPoolManager.SpawnObject(_projectilePrefab, _projectileSpawn.position, Quaternion.identity, ObjectPoolManager.PoolType.PlayerProjectile);

                    if (Time.time > _soundCooldown)
                    {
                        SoundManager.Instance.PlaySound("Shoot", 0.2f);

                        _soundCooldown = Time.time + _soundPlayTimer;
                    }

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
