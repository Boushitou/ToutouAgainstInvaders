using Systems.Pooling;
using UnityEngine;

namespace Enemies.Attack
{
    public class BlueChalienAttack : EnemyAttack
    {
        private void Start()
        {
            _atkSpeed = 0.3f;
        }

        public override void EnemyShoot()
        {
            if (Time.time > _coolDown)
            {
                ObjectPoolManager.SpawnObject(_bulletPrefab, _bulletSpawn.position, _atkRotation, ObjectPoolManager.PoolType.EnemyProjectile);
                _coolDown = Time.time + _atkSpeed;
            }
        }
    }
}
