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
        public enum PatternType
        {
            Circle,
            Spiral,
            None
        }

        public PatternType _patternType;

        public float startAngle, endAngle;
        public int bulletAmount;

        private void Awake()
        {
            _myTransform = transform;
        }

        private void Update()
        {
            EnemyShoot();
        }

        public void EnemyShoot()
        {
            if (Time.time > _coolDown)
            {
                if (_patternType == PatternType.Circle)
                {
                    CircleAttack();
                }
                else
                {
                    ObjectPoolManager.SpawnObject(_bulletPrefab, _bulletSpawn.position, _atkRotation, ObjectPoolManager.PoolType.EnemyProjectile);
                }
                _coolDown = Time.time + _atkSpeed;
            }
        }

        public void CircleAttack()
        {
            float angleStep = (endAngle - startAngle) / bulletAmount;
            float angle = startAngle;

            for (int i = 0; i < bulletAmount + 1; i++)
            {
                float bulletDirX = _myTransform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                float bulletDirY = _myTransform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

                Vector3 bulletVector = new Vector3(bulletDirX, bulletDirY, 0f);
                Vector2 bulletDir = (bulletVector - _myTransform.position).normalized;

                GameObject bullet = ObjectPoolManager.SpawnObject(_bulletPrefab, _myTransform.position, Quaternion.identity, ObjectPoolManager.PoolType.EnemyProjectile);
                bullet.GetComponent<EnemyProjectile>().SetDirection(bulletDir);

                angle += angleStep;
            }
        }
    }
}
