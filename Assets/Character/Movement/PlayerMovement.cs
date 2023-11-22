using UnityEngine;

namespace Character.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;

        private Transform _myTransform;
        private Vector2 _direction = Vector2.zero;
        private bool _isMoving = false;

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
            Movement();
        }

        public void Movement()
        {
            if (_isMoving)
            {
                _myTransform.position += new Vector3(_direction.x, _direction.y) * _speed * Time.deltaTime;
            }
            _myTransform.position = new Vector3(Mathf.Clamp(_myTransform.position.x, _minBound.x, _maxBound.x), Mathf.Clamp(_myTransform.position.y, _minBound.y, _maxBound.y));
        }

        public void SetMovement(Vector2 direction, bool isMoving)
        {
            _direction = direction;
            _isMoving = isMoving;

        }
    }
}
