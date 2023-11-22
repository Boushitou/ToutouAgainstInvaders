using Systems.Pooling;
using UnityEngine;

namespace Character.Attack
{
    public class PlayerProjectile : MonoBehaviour
    {
        private float _speed = 50f;
        private Transform _myTransform;
        private Camera _cam;

        private Vector2 _minBound;
        private Vector2 _maxBound;

        private void Awake()
        {
            _myTransform = transform;
            _cam = Camera.main;

            _minBound = _cam.ViewportToWorldPoint(new Vector2(0, 0));
            _maxBound = _cam.ViewportToWorldPoint(new Vector2(1, 1));
        }

        private void Update()
        {
            ProjectileMovement();
            ProjectileDestroy();
        }

        public void ProjectileMovement()
        {
            _myTransform.position += Vector3.up * _speed * Time.deltaTime;
        }

        public void ProjectileDestroy()
        {
            if (_myTransform.position.y < _minBound.y || _myTransform.position.y > _maxBound.y)
            {
                ProjectilePoolManager.ReturnObjectPool(gameObject);
            }
        }
    }
}