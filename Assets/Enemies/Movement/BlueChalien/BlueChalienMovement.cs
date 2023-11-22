using UnityEngine;

namespace Enemies.Movement
{
    public class BlueChalienMovement : EnemyMovement
    {
        private float _amplitude = 3f;
        private float _frequency = 5f;
        private float _elapsed = 0f;

        private void Start()
        {
            _speed = 3f;
        }

        public override void Movement()
        {
            _elapsed += Time.deltaTime;

            float horizontalOffset = Mathf.Sin(_elapsed * _frequency) * _amplitude;
            float sinWave = -1 * horizontalOffset;

            _myTransform.position += new Vector3 (sinWave, -1, 0) * _speed * Time.deltaTime;
        }
    }
}
