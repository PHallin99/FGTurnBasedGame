using GameInput;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public InputManager instance;
        public PlayerInput Input;

        private Vector3 movementDirection;

        private void Awake()
        {
            instance = this;
            Input = new PlayerInput();
        }

        private void Update()
        {
            movementDirection = Input.GetMovementInput();
        }
    }
}