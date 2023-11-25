using Systems.Pooling;
using Systems.EntityState;
using UnityEngine;
using Systems;

namespace Character.Attack
{
    public class PlayerProjectile : MonoBehaviour
    {
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null)
            {
                //Debug.Log("collide with enemy");
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    collision.gameObject.GetComponent<Health>().TakeDamage(1);
                    ObjectPoolManager.ReturnObjectPool(gameObject);
                }
            }
        }
    }
}
