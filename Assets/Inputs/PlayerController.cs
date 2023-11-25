using Character.Attack;
using Character.Movement;
using Systems.EntityState;
using Systems.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        private Health _playerHealth;

        private void Awake()
        {
            _playerHealth = _player.GetComponent<Health>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (!_playerHealth.GetIsDead())
            {
                PlayerMovement playerMovement = _player.GetComponent<PlayerMovement>();

                if (context.performed)
                {
                    playerMovement.SetMovement(context.ReadValue<Vector2>(), true);
                }
                else if (context.canceled)
                {
                    playerMovement.SetMovement(Vector2.zero, true);
                }
            }
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (!_playerHealth.GetIsDead())
            {
                PlayerAttack playerAttack = _player.GetComponent<PlayerAttack>();

                if (context.started)
                {
                    playerAttack.SetIsShooting();
                }
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (!_playerHealth.GetIsDead())
            {
                if (context.started)
                {
                    UIManager.Instance.OpenClosePauseMenu();
                }
            }
        }
    }
}
