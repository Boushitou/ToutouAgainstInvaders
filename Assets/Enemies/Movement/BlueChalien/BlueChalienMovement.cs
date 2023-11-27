using UnityEngine;

namespace Enemies.Movement
{
    public class BlueChalienMovement : EnemyMovement
    {
        private void Start()
        {
            _speed = 0.8f;
        }

        public override void Movement()
        {
            _myTransform.position += Vector3.down * _speed * Time.deltaTime;
        }
    }
}
