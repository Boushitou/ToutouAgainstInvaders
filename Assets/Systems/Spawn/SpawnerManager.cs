using Sound;
using System.Collections;
using Systems.Pooling;
using Systems.UI;
using UnityEngine;

namespace Systems.Spawn
{
    /* IMPORTANT, KEEP AN EMPTY WAVE SO THE GAME OVER SCREEN DONT OPEN BEFORE THE BOSS IS DEAD ! */
    [System.Serializable]
    public class Wave
    {
        public string _waveName;
        public GameObject[] _enemies;
        public Vector3[] _positions;
    }

    public class SpawnerManager : MonoBehaviour
    {
        public static SpawnerManager Instance;

        [SerializeField] private Wave[] _waves;

        private Wave _currentWave;
        private int _currentWaveNb = 0;
        private int _enemiesLeft = 0;

        [HideInInspector] public bool _bossWave;

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

            if (_waves.Length > 0)
            {
                _currentWave = _waves[_currentWaveNb];
            }
        }

        private void Start()
        {
            if (_currentWave != null)
            {
                PreInstantianteEnemies();
                StartCoroutine(SpawnWaves());
            }

            //Debug.Log(_waves.Length);
        }

        public IEnumerator SpawnWaves()
        {
            int index = 0;

            while (index < _waves.Length)
            {
                if (_enemiesLeft <= 0)
                {
                    Debug.Log(_currentWave._waveName);

                    if (_currentWave._waveName == "Boss Wave" && !_bossWave)
                    {
                        _bossWave = true;
                        SoundManager.Instance.PlayMusic("Boss");
                    }

                    index++;
                    _enemiesLeft = _waves[_currentWaveNb]._enemies.Length;

                    for (int i = 0; i < _currentWave._enemies.Length; i++)
                    {
                        ObjectPoolManager.SpawnObject(_currentWave._enemies[i], _currentWave._positions[i], Quaternion.identity, ObjectPoolManager.PoolType.Enemies);
                    }

                    _currentWaveNb++;

                    if (_currentWaveNb < _waves.Length)
                    {
                        _currentWave = _waves[_currentWaveNb];
                    }
                }
                yield return new WaitForSeconds(2f);
            }

            Debug.Log("Waves over !");
        }

        public void RemoveEnemy()
        {
            _enemiesLeft--;
        }

        public void PreInstantianteEnemies()
        {
            foreach (Wave wave in _waves)
            {
                foreach (GameObject enemy in wave._enemies)
                {
                    ObjectPoolManager.SpawnObject(enemy, new Vector3(0, 100, 0), Quaternion.identity, ObjectPoolManager.PoolType.Enemies);
                }
            }

            foreach (Transform enemy in GameObject.Find("Enemies").transform)
            {
                //Does not work when I put it inside the other foreach right after the SpawnObject.
                ObjectPoolManager.ReturnObjectPool(enemy.gameObject);
            }
        }
    }
}
