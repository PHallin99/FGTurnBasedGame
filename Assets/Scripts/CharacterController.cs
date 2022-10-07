using System.Collections.Generic;
using Managers;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] [Range(1, 1000)] private int moveLenghtMax;
    private readonly List<PlayableCharacter> playableCharacters = new List<PlayableCharacter>();
    private int characterIndex;
    private int moveLenght;
    private Vector3 movementDirection;
    public PlayableCharacter CurrentCharacter { get; private set; }

    private void Awake()
    {
        foreach (var child in GetComponentsInChildren<PlayableCharacter>()) playableCharacters.Add(child);
        characterIndex = 0;
        CurrentCharacter = playableCharacters[characterIndex];
        moveLenght = moveLenghtMax;
    }

    private void Update()
    {
        movementDirection = Game.InputManager.GetMovementDirection();
    }

    private void FixedUpdate()
    {
        if (movementDirection != Vector3.zero) MoveCharacter();
        if (CurrentCharacter.CanJump && Game.InputManager.GetJumpInput())
            JumpCharacter();
    }

    private void LateUpdate()
    {
        CurrentCharacter.StatusText.UpdateText(moveLenght.ToString());
    }

    public void NextCharacter()
    {
        characterIndex = characterIndex == playableCharacters.Count ? characterIndex = 0 : characterIndex++;
        CurrentCharacter = playableCharacters[characterIndex];
        Game.CharacterSwapping.SwapCharacter(CurrentCharacter);
    }

    public void PreviousCharacter()
    {
        characterIndex = characterIndex == 0 ? characterIndex = playableCharacters.Count : characterIndex--;
        CurrentCharacter = playableCharacters[characterIndex];
        Game.CharacterSwapping.SwapCharacter(CurrentCharacter);
    }

    private void JumpCharacter()
    {
        CurrentCharacter.Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        CurrentCharacter.FlipCanJump();
    }

    private void MoveCharacter()
    {
        if (moveLenght <= 0) return;
        CurrentCharacter.transform.Translate(movementDirection * movementSpeed * Time.fixedDeltaTime);
        moveLenght--;
    }
}