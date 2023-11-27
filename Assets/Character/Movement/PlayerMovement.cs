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

        private Transform _myTransform;
        private Vector2 _direction = Vector2.zero;
        private bool _isMoving = false;


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
                    GetComponentInParent<Health>().TakeDamage(collision.GetComponent<EnemyProjectile>().GetDamagge());
                    ObjectPoolManager.ReturnObjectPool(collision.gameObject);
                }
            }
        }
    }
}
