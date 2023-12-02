using Enemies.Attack;
using Systems;
using Systems.EntityState;
using Systems.Pooling;
using Unity.VisualScripting;
using UnityEngine;

namespace Character.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private GameObject _bloodParticles;

        private Transform _myTransform;
        private Vector2 _direction = Vector2.zero;
        private bool _isMoving = false;

        private float _contactDmgTimer = 0.2f;
        private float _contactDmgCoolDown = 0f;

        private void Awake()
        {
            _myTransform = transform;
        }

        private void Update()
        {
            Movement();
        }

        public void Movement()
        {
            if (_isMoving)
            {
                _myTransform.position += new Vector3(_direction.x, _direction.y) * _speed * Time.deltaTime;
            }
            _myTransform.position = new Vector3(Mathf.Clamp(_myTransform.position.x, CameraManager.Instance.GetMinBound().x, 
                CameraManager.Instance.GetMaxBound().x), Mathf.Clamp(_myTransform.position.y,
                CameraManager.Instance.GetMinBound().y, CameraManager.Instance.GetMaxBound().y));
        }

        public void SetMovement(Vector2 direction, bool isMoving)
        {
            _direction = direction;
            _isMoving = isMoving;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null)
            {
                if (collision.gameObject.CompareTag("EnemyBullet"))
                {
                    GetComponent<Health>().TakeDamage(collision.GetComponent<EnemyProjectile>().GetDamagge());
                    ObjectPoolManager.ReturnObjectPool(collision.gameObject);
                    StartCoroutine(CameraManager.Instance.ShakeScreen(0.2f));
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision != null)
            {
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    if (Time.time > _contactDmgCoolDown)
                    {
                        GetComponent<Health>().TakeDamage(1);
                        StartCoroutine(CameraManager.Instance.ShakeScreen(0.1f));
                        _contactDmgCoolDown = Time.time + _contactDmgTimer;
                    }
                }
            }
        }
    }
}
