using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    private Vector2 _direction = Vector2.zero;
    private bool _isMoving = false;

    private void Update()
    {
        Movement();
    }

    public void Movement()
    {
        if (_isMoving)
        {
            transform.position += new Vector3(_direction.x, _direction.y) * _speed * Time.deltaTime;
        }
    }

    public void SetMovement(Vector2 direction, bool isMoving)
    {
        _direction = direction;
        _isMoving = isMoving;
    }
}
