using UnityEngine;

namespace Enemies.Movement
{
    public class GreenChalienMovement : EnemyMovement
    {
        public float _amplitude = 8f;
        public float _frequency = 2f;
        private float _elapsed = 0f;

        private void Start()
        {
            _speed = 1f;
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
