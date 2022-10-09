using System;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] [Range(1, 50)] private float sensitivityHorizontal;
        [SerializeField] [Range(1, 50)] private float sensitivityVertical;
        private bool endTurnInput;
        private bool inputEnabled;
        private float mouseAxisX;
        private float mouseAxisY;
        private Vector3 movementDirection;
        private bool shouldJump;
        private bool startTurnInput;

        private void Update()
        {
            if (!inputEnabled)
            {
                startTurnInput = Input.GetKey(KeyCode.Backspace);
            }
            else
            {
                movementDirection = GetMovementInput();
                shouldJump = Input.GetKey(KeyCode.Space);
                mouseAxisY = Input.GetAxis("Mouse Y") * sensitivityVertical * Time.deltaTime;
                mouseAxisX = Input.GetAxis("Mouse X") * sensitivityHorizontal * Time.deltaTime;
                endTurnInput = Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Return);
            }
        }

        public void InputEnabled(bool state)
        {
            endTurnInput = !state;
            inputEnabled = state;
            mouseAxisX = state ? mouseAxisX : 0;
            mouseAxisY = state ? mouseAxisY : 0;
            movementDirection = state ? movementDirection : Vector3.zero;
            shouldJump = state;
            startTurnInput = state;
        }

        public static void ToggleMouseLocked()
        {
            switch (Cursor.lockState)
            {
                case CursorLockMode.None:
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                case CursorLockMode.Locked:
                case CursorLockMode.Confined:
                    Cursor.lockState = CursorLockMode.None;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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

        public float GetMouseY()
        {
            return mouseAxisY;
        }

        public float GetMouseX()
        {
            return mouseAxisX;
        }

        public bool GetEndTurnInput()
        {
            return endTurnInput;
        }

        public bool GetStartTurnInput()
        {
            return startTurnInput;
        }
    }
}