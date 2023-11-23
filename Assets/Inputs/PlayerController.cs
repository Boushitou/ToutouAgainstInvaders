using Character.Attack;
using Character.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject _player;

        public void OnMove(InputAction.CallbackContext context)
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

        public void OnShoot(InputAction.CallbackContext context)
        {
            PlayerAttack playerAttack = _player.GetComponent<PlayerAttack>();

            if (context.started)
            {
                playerAttack.SetIsShooting();
            }
        }
    }
}
