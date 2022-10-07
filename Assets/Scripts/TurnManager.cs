using Managers;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private TMP_Text nextTurnText;
    private int playersPerTurn;
    private int playerTurnIndex;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!nextTurnText.gameObject.activeSelf && Game.InputManager.GetEndTurnInput()) EndTurn();
        if (nextTurnText.gameObject.activeSelf && Game.InputManager.GetStartTurnInput()) StartTurn();
    }

    public void InitializeGame(int totalPlayersPerTurn)
    {
        playersPerTurn = totalPlayersPerTurn;
    }

    private void StartTurn()
    {
        nextTurnText.gameObject.SetActive(false);
        Game.InputManager.InputEnabled(true);
    }

    private void EndTurn()
    {
        nextTurnText.gameObject.SetActive(true);
        nextTurnText.text = $"Player {playerTurnIndex + 1} turn! \n Press Backspace To Start";
        if (playerTurnIndex == playersPerTurn - 1)
        {
            playerTurnIndex = 0;
        }

        else
        {
            playerTurnIndex++;
        }

        Game.InputManager.InputEnabled(false);
    }
}