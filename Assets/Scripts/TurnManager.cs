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
        if (!gameObject.activeSelf || !Input.GetKey(KeyCode.KeypadEnter)) return;
        StartTurn();
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

    public void EndTurn()
    {
        playerTurnIndex = playerTurnIndex == playersPerTurn ? playerTurnIndex = 0 : playerTurnIndex++;
        nextTurnText.gameObject.SetActive(true);
        nextTurnText.text = $"Player {playerTurnIndex + 1} turn! \n Press Enter To Start";
        Game.InputManager.InputEnabled(false);
    }
}