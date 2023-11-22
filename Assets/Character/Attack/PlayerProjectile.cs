using Systems.Pooling;
using UnityEngine;

namespace Character.Attack
{
    public class PlayerProjectile : MonoBehaviour
    {
        private float _speed = 50f;

        private float _timerTest = 0;

        private void Update()
        {
            ProjectileMovement();

            _timerTest += Time.deltaTime;

            if (_timerTest > 3f)
            {
                _timerTest = 0;
                ProjectilePoolManager.ReturnObjectPool(gameObject);
            }
        }

        public void ProjectileMovement()
        {
            transform.position += Vector3.up * _speed * Time.deltaTime;
        }
    }
}
