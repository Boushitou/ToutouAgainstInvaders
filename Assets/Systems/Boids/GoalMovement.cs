using Systems.Pooling;
using Systems.Spawn;
using UnityEngine;

namespace Systems.Boids
{
    public class GoalMovement : MonoBehaviour
    {
        public float _amplitude = 8f;
        public float _frequency = 2f;
        public float _speed;
        private float _elapsed = 0f;

        private Transform _myTransform;

        private void Awake()
        {
            _myTransform = transform;
        }

        private void Update()
        {
            Movement();

            DestroyGoal();
        }

        public void Movement()
        {
            _elapsed += Time.deltaTime;

            float horizontalOffset = Mathf.Sin(_elapsed * _frequency) * _amplitude;
            float sinWave = -1 * horizontalOffset;

            _myTransform.position += new Vector3(sinWave, -1, 0) * _speed * Time.deltaTime;
        }

        public void DestroyGoal()
        {
            if (_myTransform.position.y < CameraManager.Instance.GetMinBound().y - 7)
            {
                Debug.Log("destroy goal");
                //ObjectPoolManager.ReturnObjectPool(gameObject);
                FlockManager.Instance._canSpawnGoal = true;
                Destroy(gameObject);
            }
        }
    }
}
