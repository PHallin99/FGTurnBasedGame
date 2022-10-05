using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager instance;

        private Vector3 movementDirection;
        private bool shouldJump;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            movementDirection = GetMovementInput();
            shouldJump = Input.GetKey(KeyCode.Space);
            if (GetTurnInput())
            {
                
            }
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

        public bool GetTurnInput()
        {
            return Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Return);
        }
    }
}