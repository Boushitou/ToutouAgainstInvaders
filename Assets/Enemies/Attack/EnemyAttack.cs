using UnityEngine;
using Systems.Pooling;
using UnityEngine.UIElements;

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
        private float _spiralAngle = 0f;
        private bool _canShoot;

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
            _canShoot = true;
        }

        private void Update()
        {
            EnemyShoot();

            Debug.Log(_canShoot);
        }

        public void EnemyShoot()
        {
            if (Time.time > _coolDown)
            {
                if (_canShoot)
                {
                    if (_patternType == PatternType.Circle)
                    {
                        CircleAttack();
                    }
                    else if (_patternType == PatternType.Spiral)
                    {
                        SpiralAttack();
                    }
                    else
                    {
                        GameObject bullet = ObjectPoolManager.SpawnObject(_bulletPrefab, _bulletSpawn.position, _atkRotation, ObjectPoolManager.PoolType.EnemyProjectile);
                        bullet.GetComponent<EnemyProjectile>().SetDirection(Vector2.down);
                    }
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

        public void SpiralAttack()
        {
            for (int i = 0; i <= 1; i++)
            {
                float bulDirX = _myTransform.position.x + Mathf.Sin((_spiralAngle + 180f * i) * Mathf.PI / 180f);
                float bulDirY = _myTransform.position.y + Mathf.Cos((_spiralAngle + 180f * i) * Mathf.PI / 180f);

                Vector3 bulletVector = new Vector3(bulDirX, bulDirY, 0f);
                Vector2 bulletDir = (bulletVector - _myTransform.position).normalized;

                GameObject bullet = ObjectPoolManager.SpawnObject(_bulletPrefab, _myTransform.position, Quaternion.identity, ObjectPoolManager.PoolType.EnemyProjectile);
                bullet.GetComponent<EnemyProjectile>().SetDirection(bulletDir);
            }

            _spiralAngle += 3f;
            if (_spiralAngle >= 360f)
            {
                _spiralAngle = 0f;
            }
        }

        public void SetAtkSpeed(float atkSpeed)
        {
            _atkSpeed = atkSpeed;
        }

        public void SetPattern(PatternType pattern)
        {
            _patternType = pattern;
        }

        public bool GetCanShoot()
        {
            return _canShoot;
        }

        public void SetCanShoot(bool canShoot)
        {
            _canShoot = canShoot;
        }
    }
}
