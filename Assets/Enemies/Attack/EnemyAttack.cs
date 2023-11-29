using UnityEngine;
using Systems.Pooling;
using UnityEngine.UIElements;
using System.Collections;

namespace Enemies.Attack
{
    public abstract class EnemyAttack : MonoBehaviour
    {
        [SerializeField] protected GameObject _bulletPrefab;
        [SerializeField] protected Transform _bulletSpawn;

        protected float _atkSpeed = 0f;
        protected Vector2 _direction = Vector2.down;
        protected Quaternion _atkRotation = Quaternion.identity;
        protected float _bulletSpeed = 8f;
        protected Transform _myTransform;

        [Header("Circle/Spiral")]
        protected float _coolDown = 0f;
        private float _spiralAngle = 0f;
        private bool _canShoot;
        private Vector3 _bulletVector = Vector3.zero;

        public enum PatternType
        {
            Circle,
            Spiral,
            None
        }

        public PatternType _patternType;

        public float _startAngle, _endAngle;
        public int _bulletAmount;

        private void Awake()
        {
            _myTransform = transform;
            _canShoot = true;
        }

        private void Update()
        {
            EnemyShoot();
        }

        public void EnemyShoot()
        {
            if (Time.time > _coolDown)
            {
                if (_canShoot)
                {
                    if (_patternType == PatternType.Circle)
                    {
                        float angleStep = (_endAngle - _startAngle) / _bulletAmount;
                        float angle = _startAngle;

                        CircleAttack(angleStep, angle, _bulletAmount, _myTransform, _bulletSpeed);
                    }
                    else if (_patternType == PatternType.Spiral)
                    {
                        SpiralAttack();
                    }
                    else
                    {
                        GameObject bullet = ObjectPoolManager.SpawnObject(_bulletPrefab, _bulletSpawn.position, _atkRotation, ObjectPoolManager.PoolType.EnemyProjectile);
                        bullet.GetComponent<EnemyProjectile>().SetDirection(_direction);
                        bullet.GetComponent<EnemyProjectile>().SetSpeed(_bulletSpeed);
                    }
                }
                _coolDown = Time.time + _atkSpeed;
            }
        }

        public void CircleAttack(float angleStep, float angle, int bulletAmount, Transform myTransform, float speed)
        {
            for (int i = 0; i < bulletAmount + 1; i++)
            {
                _bulletVector.x = myTransform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                _bulletVector.y = myTransform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

                Vector2 bulletDir = (_bulletVector - myTransform.position).normalized;

                GameObject bullet = ObjectPoolManager.SpawnObject(_bulletPrefab, myTransform.position, Quaternion.identity, ObjectPoolManager.PoolType.EnemyProjectile);
                bullet.GetComponent<EnemyProjectile>().SetDirection(bulletDir);
                bullet.GetComponent<EnemyProjectile>().SetSpeed(speed);

                angle += angleStep;
            }
        }

        public IEnumerator LaunchCircleAttack(int shootNb, float atkSpeed, float angleStep, float angle, int bulletAmount, Transform myTransform)
        {
            for (int i = 0; i <= shootNb; i++)
            {
                CircleAttack(angleStep, angle, bulletAmount, myTransform, 15f);

                yield return new WaitForSeconds(atkSpeed);
            }
        }

        public void SpiralAttack()
        {
            for (int i = 0; i <= 1; i++)
            {
                _bulletVector.x = _myTransform.position.x + Mathf.Sin((_spiralAngle + 180f * i) * Mathf.PI / 180f);
                _bulletVector.y = _myTransform.position.y + Mathf.Cos((_spiralAngle + 180f * i) * Mathf.PI / 180f);

                Vector2 bulletDir = (_bulletVector - _myTransform.position).normalized;

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

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void SetBulletSpeed(float bulletSpeed)
        { 
            _bulletSpeed = bulletSpeed;
        }
    }
}
