using System;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] [Range(1, 200)] private float sensitivityHorizontal;
        private bool inputEnabled;

        public bool ActionInputsEnabled { get; set; }
        public Vector3 MovementDirection { get; private set; }
        public bool JumpPressing { get; private set; }
        public float MouseAxisX { get; private set; }
        public bool NextCharacterPressed { get; private set; }
        public bool PrevCharacterPressed { get; private set; }
        public bool PrimaryFirePressed { get; private set; }
        public bool PrimaryFireReleased { get; private set; }
        public bool SecondaryFirePressed { get; private set; }
        public bool EndTurnPressed { get; private set; }
        public bool StartTurnPressed { get; private set; }
        public bool SecondaryFireReleased { get; private set; }
        public bool ModifierPressed { get; private set; }

        private void Update()
        {
            if (!inputEnabled)
            {
                StartTurnPressed = Input.GetKey(KeyCode.Backspace);
            }
            else
            {
                ReadInputs();
            }

            if (ActionInputsEnabled)
            {
                ReadActionInputs();
            }
        }

        private void ReadActionInputs()
        {
            PrimaryFirePressed = Input.GetKeyDown(KeyCode.Mouse0);
            SecondaryFirePressed = Input.GetKeyDown(KeyCode.Mouse1);
            PrimaryFireReleased = Input.GetKeyUp(KeyCode.Mouse0);
            SecondaryFireReleased = Input.GetKeyUp(KeyCode.Mouse1);
            EndTurnPressed = Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Return);
            ModifierPressed = Input.GetKey(KeyCode.LeftControl);
            NextCharacterPressed = Input.GetKeyDown(KeyCode.Tab);
            PrevCharacterPressed = Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Tab);
        }

        private void ReadInputs()
        {
            MovementDirection = GetMovementInput();
            JumpPressing = Input.GetKeyDown(KeyCode.Space);
            MouseAxisX = Input.GetAxis("Mouse X") * sensitivityHorizontal * Time.deltaTime;
        }

        public void SetInputEnabled(bool state)
        {
            ActionInputsEnabled = state;
            inputEnabled = state;
            EndTurnPressed = !state;
            MouseAxisX = state ? MouseAxisX : 0;
            MovementDirection = state ? MovementDirection : Vector3.zero;
            JumpPressing = false;
            PrimaryFirePressed = false;
            StartTurnPressed = false;
            ModifierPressed = false;
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

        private static Vector3 GetMovementInput()
        {
            var movementInput = new Vector3();
            movementInput = Input.GetKey(KeyCode.Q) ? movementInput + Vector3.left : movementInput;
            movementInput = Input.GetKey(KeyCode.W) ? movementInput + Vector3.forward : movementInput;
            movementInput = Input.GetKey(KeyCode.E) ? movementInput + Vector3.right : movementInput;
            movementInput = Input.GetKey(KeyCode.S) ? movementInput + Vector3.back : movementInput;
            return movementInput;
        }
    }
}