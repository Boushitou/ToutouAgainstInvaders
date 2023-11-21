using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _playerMovement.SetMovement(context.ReadValue<Vector2>(), true);
            }
            else if (context.canceled)
            {
                _playerMovement.SetMovement(Vector2.zero, true);
            }
        }
    }
}
