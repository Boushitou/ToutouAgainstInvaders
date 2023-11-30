using BehaviourTree;
using System.Collections;
using Systems.Pooling;
using Systems.Spawn;
using Systems.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.EntityState
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _scoreAmount;
        [SerializeField] private GameObject _bloodParticles;

        private int _currentHealth;

        private bool _isDead;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        private void Start()
        {
            if (!gameObject.CompareTag("Enemy"))
            {
                UIManager.Instance.UpdateHpBar(_currentHealth, _maxHealth);
            }
        }

        public void TakeDamage(int damage)
        {
            if (!_isDead) 
            {
                _currentHealth -= damage;

                Death();

                if (!gameObject.CompareTag("Enemy"))
                {
                    UIManager.Instance.UpdateHpBar(_currentHealth, _maxHealth);
                }

                //Debug.Log("Inflicted: " + damage + " damage");
                //Debug.Log("Current health: " + _currentHealth);
            }
        }

        public void Heal(int healAmount)
        {
            if (!_isDead)
            {
                _currentHealth += healAmount;

                if (_currentHealth > _maxHealth)
                {
                    _currentHealth = _maxHealth;
                }

                if (!gameObject.CompareTag("Enemy"))
                {
                    UIManager.Instance.UpdateHpBar(_currentHealth, _maxHealth);
                }

                //Debug.Log("Healed: " + healAmount + " hp");
                //Debug.Log("Current health: " + _currentHealth);
            }
        }

        public void Death()
        {
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                _isDead = true;

                ObjectPoolManager.SpawnObject(_bloodParticles, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Particules);

                Debug.Log(gameObject.name + " is dead !");

                if (gameObject.CompareTag("Enemy"))
                {
                    if (TryGetComponent(out BossBT boss))
                    {
                        boss.DeathAnimation();
                        StartCoroutine(PoofExplosion());
                    }
                    else
                    {
                        SpawnerManager.Instance.RemoveEnemy();
                        UIManager.Instance.AddScore(_scoreAmount);
                        ObjectPoolManager.ReturnObjectPool(gameObject);
                    }
                }
                else
                {
                    string gameOverTxt = "You died !";
                    Destroy(gameObject);
                    UIManager.Instance.OpenGameOverMenu(gameOverTxt);
                }
            }
        }

        public IEnumerator PoofExplosion()
        {
            while (_isDead)
            {
                Vector2 poofPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));

                ObjectPoolManager.SpawnObject(_bloodParticles, poofPos, Quaternion.identity, ObjectPoolManager.PoolType.Particules);

                yield return new WaitForSeconds(0.2f);
            }
        }

        public bool GetIsDead()
        {
            return _isDead;
        }

        public float GetHealthPercentage()
        {
            return ((float)_currentHealth / _maxHealth) * 100;
        }

        public int GetCurrentHealth()
        {
            return _currentHealth;
        }

        public int GetScoreAmount()
        {
            return _scoreAmount;
        }
    }
}
