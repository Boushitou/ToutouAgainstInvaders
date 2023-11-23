using Systems.Pooling;
using Systems.Spawn;
using UnityEngine;

namespace Systems.EntityState
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        private int _currentHealth;

        private bool _isDead;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (!_isDead) 
            {
                _currentHealth -= damage;

                if (_currentHealth <= 0)
                {
                    _currentHealth = 0;
                    _isDead = true;

                    Debug.Log(gameObject.name + " is dead !");
                    
                    if (gameObject.CompareTag("Enemy"))
                    {
                        SpawnerManager.Instance.RemoveEnemy();
                        ObjectPoolManager.ReturnObjectPool(gameObject);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
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

                //Debug.Log("Healed: " + healAmount + " hp");
                //Debug.Log("Current health: " + _currentHealth);
            }
        }

        public bool GetIsDead()
        {
            return _isDead;
        }
    }
}
