using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [FormerlySerializedAs("sensitivity")] [SerializeField] [Range(1, 15)] private float sensitivityHorizontal;
    [FormerlySerializedAs("sensitivity")] [SerializeField] [Range(1, 15)] private float sensitivityVertical;
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
        var mouseY = Input.GetAxis("Mouse Y");
        var mouseX = Input.GetAxis("Mouse X");
        if (mouseY == 0 && mouseX == 0) return;
        currentCharacter.transform.Rotate(Vector3.up, mouseX * sensitivityHorizontal * Time.deltaTime, Space.World);
        currentCharacter.WeaponTransform.transform.Rotate(Vector3.right, -mouseY * sensitivityVertical * Time.deltaTime, Space.World);
    }
}