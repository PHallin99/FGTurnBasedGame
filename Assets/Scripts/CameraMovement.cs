using Cinemachine;
using Managers;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private CharacterController charController;

    private void Awake()
    {
        charController = FindObjectOfType<CharacterController>();
    }

    private void Start()
    {
        var characterTransform = charController.CurrentCharacter.transform;
        virtualCamera.Follow = characterTransform;
        virtualCamera.LookAt = characterTransform;
    }

    private void Update()
    {
        var currentCharacter = charController.CurrentCharacter;

        currentCharacter.transform.Rotate(Vector3.up, Game.InputManager.GetMouseX(), Space.World);
        currentCharacter.WeaponTransform.Rotate(Vector3.right, -Game.InputManager.GetMouseY());
        currentCharacter.WeaponTransform.forward =
            ClampVector(currentCharacter.WeaponTransform.forward, Vector3.zero, 22.5f);
    }

    private static Vector3 ClampVector(Vector3 direction, Vector3 center, float maxAngle)
    {
        var angle = Vector3.Angle(center, direction);
        if (!(angle > maxAngle)) return direction;
        direction.Normalize();
        center.Normalize();
        var rotation = (direction - center) / angle;
        return rotation * maxAngle + center;
    }
}