public class PlayerTurns
{
    private int playersPerTurn;
    private int playerTurnIndex;

    public void InitializeGame(int totalPlayersPerTurn)
    {
        playersPerTurn = totalPlayersPerTurn;
    }

    public void EndTurn()
    {
        playerTurnIndex = playerTurnIndex == playersPerTurn ? playerTurnIndex = 0 : playerTurnIndex++;
    }
}