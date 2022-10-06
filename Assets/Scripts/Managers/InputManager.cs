using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        private Vector3 movementDirection;
        private bool shouldJump;
        private bool inputEnabled = false;

        private void Update()
        {
            if (!inputEnabled) return;

            movementDirection = GetMovementInput();
            shouldJump = Input.GetKey(KeyCode.Space);
            if (GetTurnInput())
            {
                // TODO: After end turn input
            }
        }

        public void InputEnabled(bool state) => inputEnabled = state;

        public Vector3 GetMovementDirection()
        {
            return movementDirection;
        }

        public bool GetJumpInput()
        {
            return shouldJump;
        }

        private static Vector3 GetMovementInput()
        {
            var movementInput = new Vector3();
            movementInput = Input.GetKey(KeyCode.Q) ? movementInput + Vector3.left : movementInput;
            movementInput = Input.GetKey(KeyCode.W) ? movementInput + Vector3.forward : movementInput;
            movementInput = Input.GetKey(KeyCode.E) ? movementInput + Vector3.right : movementInput;
            movementInput = Input.GetKey(KeyCode.S) ? movementInput + Vector3.back : movementInput;
            return movementInput;
        }

        private static bool GetTurnInput()
        {
            return Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Return);
        }
    }
}