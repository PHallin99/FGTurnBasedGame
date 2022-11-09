using System;
using Cinemachine;
using Enums;
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

    private void Update()
    {
        HandleRotation();
    }

    public void UpdateCamera()
    {
        switch (Game.TurnManager.gamePhase)
        {
            case GamePhase.PreAction:
            {
                var characterTransform = charController.CurrentCharacter.transform;
                virtualCamera.Follow = characterTransform;
                virtualCamera.LookAt = characterTransform;
                break;
            }
            case GamePhase.PostAction when Game.TurnManager.ActionProjectile == null:
                return;
            case GamePhase.PostAction:
            {
                var projectile = Game.TurnManager.ActionProjectile.gameObject;
                virtualCamera.Follow = projectile.transform;
                virtualCamera.LookAt = projectile.transform;
                break;
            }
            case GamePhase.TurnEnded:
                virtualCamera.Follow = null;
                virtualCamera.LookAt = null;
                break;
        }
    }

    private void HandleRotation()
    {
        // TODO: try transform.rotation.eulerAngles
        charController.CurrentCharacter.transform.Rotate(Vector3.up, Game.InputManager.MouseAxisX, Space.World);
    }
}