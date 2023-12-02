using System.Collections.Generic;
using Systems.Pooling;
using Systems.Spawn;
using UnityEngine;

namespace Systems.Boids
{
    public class FlockManager : MonoBehaviour
    {
        [SerializeField] private GameObject _boidPrefab;
        [SerializeField] private GameObject _goalPrefab;
        [HideInInspector] public GameObject _goal;
        [Range(0f, 10f)]

        public static FlockManager Instance;

        public int _numBoid;
        //public GameObject[] _allBoids;
        [HideInInspector] public List<GameObject> _allBoids;
        [HideInInspector] public Vector3 _moveLimit = new Vector3(10, 10, 10);

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

        [HideInInspector] public bool _canSpawnGoal = true;

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
            _goal = null;
        }

        private void Start()
        {
            SpawnGoal();
            SpawnBoid();
        }

        private void Update()
        {
            if (_goal == null && !SpawnerManager.Instance._bossWave)
            {
                if (Random.Range(0, 100+1) < 5)
                {
                    _numBoid = Random.Range(20, 40);
                    SpawnGoal();
                    SpawnBoid();
                }
            }
        }

        public void SpawnBoid()
        {
            _allBoids = new List<GameObject>();

            for (int i = 0; i < _numBoid; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-_moveLimit.x, _moveLimit.x),
                                          Random.Range(-_moveLimit.y, _moveLimit.y) + 15f, 0);

                ObjectPoolManager.SpawnObject(_boidPrefab, pos, Quaternion.identity, ObjectPoolManager.PoolType.Boids);
            }
        }

        public void SpawnGoal()
        {
            Debug.Log("spawn goal");
            Vector3 spawnPos = new Vector3(21f, 15f, 0f);

            _goal = Instantiate(_goalPrefab, spawnPos, Quaternion.identity);

            _canSpawnGoal = false;
        }
    }

}