using UnityEngine;
using Systems.Pooling;

namespace Enemies.Attack
{
    public abstract class EnemyAttack : MonoBehaviour
    {
        [SerializeField] protected GameObject _bulletPrefab;
        [SerializeField] protected Transform _bulletSpawn;

        protected float _atkSpeed = 0f;
        protected Quaternion _atkRotation = Quaternion.identity;
        protected Transform _myTransform;

        protected float _coolDown = 0f;

        private void Awake()
        {
            _myTransform = transform;
        }

        private void Update()
        {
            EnemyShoot();
        }

        public abstract void EnemyShoot();
    }
}
