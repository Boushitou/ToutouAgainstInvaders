using UnityEngine;

namespace Systems.EntityState
{
    public class State : MonoBehaviour
    {
        private Health _health;
        private int _playerMaxHealth = 100;

        private void Awake()
        {
            _health = new Health(_playerMaxHealth);
        }

        public Health GetPlayerHealth()
        {
            return _health;
        }
    }
}
