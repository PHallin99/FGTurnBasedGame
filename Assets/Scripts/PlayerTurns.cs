public class PlayerTurns
{
    private readonly int playersPerTurn;
    private int playerTurnIndex;

    public PlayerTurns(int totalPlayersPerTurn)
    {
        playersPerTurn = totalPlayersPerTurn;
    }
    
    public void EndTurn()
    {
        playerTurnIndex = playerTurnIndex == playersPerTurn ? playerTurnIndex = 0 : playerTurnIndex++;
        
    }
}