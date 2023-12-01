using Systems.Pooling;
using UnityEngine;

namespace Systems.Boids
{
    public class FlockManager : MonoBehaviour
    {
        [SerializeField] private GameObject _boidPrefab;
        public GameObject _goalPos;
        [Range(0f, 10f)]
        public float _goalWeight;

        public static FlockManager Instance;

        public int _numBoid;
        public GameObject[] _allBoids;
        public Vector3 _moveLimit = new Vector3(10, 10, 10);

        [Header("Boid Settings")]
        [Range(0f, 10f)]
        public float _minSpeed;
        [Range(0f, 10f)]
        public float _maxSpeed;
        [Range(1f, 30f)]
        public float _neighbourDistance;
        [Range(1f, 30f)]
        public float _separationDistance;
        [Range(1f, 20f)]
        public float _steeringSpeed;

        private float timerTest = 2f;
        private float cooldownTest = 0f;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            SpawnBoid();
        }

        private void Update()
        {
            if (Time.time > cooldownTest)
            {
                //_goalPos.transform.position = new Vector3(Random.Range(-_moveLimit.x, _moveLimit.x),
                //                                          Random.Range(-_moveLimit.y, _moveLimit.y), 0);

                cooldownTest = Time.time + timerTest;
            }
        }

        public void SpawnBoid()
        {
            _allBoids = new GameObject[_numBoid];

            for (int i = 0; i < _numBoid; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-_moveLimit.x, _moveLimit.x),
                                          Random.Range(-_moveLimit.y, _moveLimit.y) + 23f, 0);

                _allBoids[i] = ObjectPoolManager.SpawnObject(_boidPrefab, pos, Quaternion.identity);
            }
        }
    }

}