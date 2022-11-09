using System.Collections;
using Enums;
using Managers;
using TMPro;
using UnityEngine;
using Weapons;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private TMP_Text nextTurnText;
    [SerializeField] private float turnPauseSeconds;

    public GamePhase gamePhase = GamePhase.PreAction;
    private int playersPerTurn;

    public Projectile ActionProjectile { get; private set; }

    public int PlayerTurnIndex { get; private set; }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        CheckTurnInputs();
    }


    private void CheckTurnInputs()
    {
        if (gamePhase == GamePhase.GameOver)
        {
            if (Game.InputManager.EndTurnPressed)
            {
                Application.Quit();
            }

            return;
        }

        switch (nextTurnText.gameObject.activeSelf)
        {
            case false when Game.InputManager.EndTurnPressed:
                nextTurnText.gameObject.SetActive(true);
                EndTurn();
                break;
            case true when Game.InputManager.StartTurnPressed:
                nextTurnText.gameObject.SetActive(false);
                StartTurn();
                break;
        }
    }

    public void SetPlayersPerTurn(int totalPlayersPerTurn)
    {
        playersPerTurn = totalPlayersPerTurn;
    }

    public void EndTurnAction(Projectile projectile)
    {
        Game.InputManager.ActionInputsEnabled = false;
        gamePhase = GamePhase.PostAction;
        ActionProjectile = projectile;
        Game.CameraMovement.UpdateCamera();
    }

    public void EndTurnPostAction()
    {
        StartCoroutine(EndTurnPause());
    }

    private void StartTurn()
    {
        gamePhase = GamePhase.PreAction;
        Game.InputManager.SetInputEnabled(true);
        Game.CameraMovement.UpdateCamera();
    }

    private void EndTurn()
    {
        gamePhase = GamePhase.TurnEnded;
        nextTurnText.gameObject.SetActive(true);
        nextTurnText.text = $"Player {PlayerTurnIndex + 1}'s turn! \n Press Backspace To Start";
        Game.InputManager.SetInputEnabled(false);
        Game.TeamsTracker.UpdateHp();
        IteratePlayerTurnIndex();
        Game.CharacterController.NewTurn();
        Game.CharacterController.IterateCharacterIndex(
            Game.CharacterController.PlayerIndexPlayableCharacters[PlayerTurnIndex]);

        Game.CameraMovement.UpdateCamera();
    }

    private void IteratePlayerTurnIndex()
    {
        PlayerTurnIndex++;
        if (PlayerTurnIndex == playersPerTurn)
        {
            PlayerTurnIndex = 0;
        }
    }

    private IEnumerator EndTurnPause()
    {
        yield return new WaitForSeconds(turnPauseSeconds);
        EndTurn();
    }
}