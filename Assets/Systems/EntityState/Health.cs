using UnityEngine;

namespace Systems.EntityState
{
    public class Health
    {
        [SerializeField] private int _maxHealth;
        private int _currentHealth;

        private bool _isDead;

        public Health(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;

            _isDead = false;
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

                    Debug.Log("Player is dead !");
                }

                Debug.Log("Inflicted: " + damage + " damage");
                Debug.Log("Current health: " + _currentHealth);
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

                Debug.Log("Healed: " + healAmount + " hp");
                Debug.Log("Current health: " + _currentHealth);
            }
        }

        public bool GetIsDead()
        {
            return _isDead;
        }
    }
}
