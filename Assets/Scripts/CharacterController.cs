using System.Collections.Generic;
using Managers;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] [Range(1, 1000)] private int moveLenghtMax;

    private readonly List<PlayableCharacter> player1PlayableCharacters = new List<PlayableCharacter>();
    private readonly List<PlayableCharacter> player2PlayableCharacters = new List<PlayableCharacter>();
    private int player1CharacterIndex;
    private int player2CharacterIndex;
    private Vector3 movementDirection;
    public PlayableCharacter CurrentCharacter { get; private set; }

    public Dictionary<int, List<PlayableCharacter>> PlayerIndexPlayableCharacters { get; } =
        new Dictionary<int, List<PlayableCharacter>>();

    public int MovementFramesLeft { get; private set; }

    public int MoveLenghtMax => moveLenghtMax;

    private void Awake()
    {
        AssignTeams();
        InitializeGame();
    }

    private void Update()
    {
        HandleMovement();
        HandleCharacterSwapping();
    }

    private void FixedUpdate()
    {
        if (movementDirection != Vector3.zero) MoveCharacter();
        if (CurrentCharacter.CanJump && Game.InputManager.JumpPressing)
            JumpCharacter();
    }

    private void LateUpdate()
    {
        CurrentCharacter.StatusText.UpdateStaminaUI(CurrentCharacter.Hp.ToString());
    }

    public void NewTurn()
    {
        MovementFramesLeft = MoveLenghtMax;
        SetCurrentCharacter();
    }

    private void SetCurrentCharacter()
    {
        if (PlayerIndexPlayableCharacters[0].Count <= 0 || PlayerIndexPlayableCharacters[1].Count <= 0)
        {
            return;
        }

        var currentPlayerTurnIndex = Game.TurnManager.PlayerTurnIndex;
        CurrentCharacter = currentPlayerTurnIndex == 0
            ? PlayerIndexPlayableCharacters[currentPlayerTurnIndex][Random.Range(0, player1PlayableCharacters.Count)]
            : PlayerIndexPlayableCharacters[currentPlayerTurnIndex][Random.Range(0, player2PlayableCharacters.Count)];
    }

    private void InitializeGame()
    {
        player1CharacterIndex = 0;
        SetCurrentCharacter();
        MovementFramesLeft = MoveLenghtMax;
    }

    private void AssignTeams()
    {
        var isTeam1 = true;
        foreach (var playableCharacter in GetComponentsInChildren<PlayableCharacter>())
        {
            switch (isTeam1)
            {
                case true:
                    player1PlayableCharacters.Add(playableCharacter);
                    playableCharacter.team = Team.Team1;
                    isTeam1 = false;
                    break;
                case false:
                    player2PlayableCharacters.Add(playableCharacter);
                    playableCharacter.team = Team.Team2;
                    isTeam1 = true;
                    break;
            }

            playableCharacter.Initialize();
        }

        PlayerIndexPlayableCharacters.Add(0, player1PlayableCharacters);
        PlayerIndexPlayableCharacters.Add(1, player2PlayableCharacters);
    }

    private void HandleCharacterSwapping()
    {
        if (Game.InputManager.NextCharacterPressed && !Game.InputManager.ModifierPressed)
        {
            NextCharacter(PlayerIndexPlayableCharacters[Game.TurnManager.PlayerTurnIndex]);
            Game.CameraMovement.UpdateCamera();
        }

        if (!Game.InputManager.PrevCharacterPressed) return;
        PreviousCharacter(PlayerIndexPlayableCharacters[Game.TurnManager.PlayerTurnIndex]);
        Game.CameraMovement.UpdateCamera();
    }

    private void HandleMovement()
    {
        movementDirection = Game.InputManager.MovementDirection;
    }

    public void IterateCharacterIndex(IReadOnlyList<PlayableCharacter> currentPlayersCharacters)
    {
        if (Equals(currentPlayersCharacters, player1PlayableCharacters))
        {
            player1CharacterIndex++;
            if (player1CharacterIndex >= currentPlayersCharacters.Count)
            {
                player1CharacterIndex = player1CharacterIndex = 0;
            }
        }

        else
        {
            player2CharacterIndex++;
            if (player2CharacterIndex >= currentPlayersCharacters.Count)
            {
                player2CharacterIndex = 0;
            }
        }
    }

    private void NextCharacter(IReadOnlyList<PlayableCharacter> currentPlayersCharacters)
    {
        IterateCharacterIndex(currentPlayersCharacters);
        switch (Game.TurnManager.PlayerTurnIndex)
        {
            case 0:
                if (player1CharacterIndex >= currentPlayersCharacters.Count)
                {
                    player1CharacterIndex = 0;
                }

                CurrentCharacter = currentPlayersCharacters[player1CharacterIndex];

                break;
            case 1:
                if (player2CharacterIndex >= currentPlayersCharacters.Count)
                {
                    player2CharacterIndex = 0;
                }

                CurrentCharacter = currentPlayersCharacters[player2CharacterIndex];
                break;
        }
    }

    private void PreviousCharacter(IReadOnlyList<PlayableCharacter> currentPlayersCharacters)
    {
        switch (Game.TurnManager.PlayerTurnIndex)
        {
            case 0:
                player1CharacterIndex--;
                if (player1CharacterIndex < 0)
                {
                    player1CharacterIndex = currentPlayersCharacters.Count - 1;
                }

                CurrentCharacter = currentPlayersCharacters[player1CharacterIndex];
                break;
            case 1:
                player2CharacterIndex--;
                if (player2CharacterIndex < 0)
                {
                    player2CharacterIndex = currentPlayersCharacters.Count - 1;
                }

                CurrentCharacter = currentPlayersCharacters[player2CharacterIndex];
                break;
        }


        CurrentCharacter = currentPlayersCharacters[player1CharacterIndex];
    }

    private void JumpCharacter()
    {
        CurrentCharacter.Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        CurrentCharacter.FlipCanJump();
    }

    private void MoveCharacter()
    {
        if (MovementFramesLeft <= 0)
        {
            Game.TurnManager.EndTurnPostAction();
            return;
        }

        CurrentCharacter.transform.Translate(movementDirection * movementSpeed * Time.fixedDeltaTime);
        MovementFramesLeft -= 1;
    }

    public void RemoveCharacter(PlayableCharacter playableCharacter)
    {
        PlayerIndexPlayableCharacters[(int)playableCharacter.team].Remove(playableCharacter);
    }
}