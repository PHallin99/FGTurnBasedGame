using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        private bool inputEnabled;
        private Vector3 movementDirection;
        private bool shouldJump;

        private void Update()
        {
            if (!inputEnabled) return;

            movementDirection = GetMovementInput();
            shouldJump = Input.GetKey(KeyCode.Space);
        }

        public void InputEnabled(bool state)
        {
            inputEnabled = state;
            movementDirection = Vector3.zero;
            shouldJump = false;
        }

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

        public bool GetEndTurnInput()
        {
            if (inputEnabled)
                return Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Return);
            return false;
        }

        public bool GetStartTurnInput()
        {
            return !inputEnabled && Input.GetKey(KeyCode.Backspace);
        }
    }
}