using UnityEngine;

namespace GameInput
{
    public class PlayerInput
    {
        public Vector3 GetMovementInput()
        {
            var movementDirection = new Vector3();
            movementDirection = Input.GetKey(KeyCode.Q) ? movementDirection + Vector3.left : movementDirection;
            movementDirection = Input.GetKey(KeyCode.W) ? movementDirection + Vector3.forward : movementDirection;
            movementDirection = Input.GetKey(KeyCode.E) ? movementDirection + Vector3.right : movementDirection;
            movementDirection = Input.GetKey(KeyCode.S) ? movementDirection + Vector3.back : movementDirection;
            return movementDirection;
        }
    }
}