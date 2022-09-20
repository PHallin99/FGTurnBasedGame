using UnityEngine;

internal class PlayableCharacter : MonoBehaviour
{
    public PlayableCharacter(Team teamAssignment)
    {
        team = teamAssignment;
    }
    
    public Team team;
    private int hitPoints;
}

public enum Team
{
    Team1,
    Team2,
    Team3,
    Team4
}