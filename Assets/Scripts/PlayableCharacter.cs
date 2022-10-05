using UI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayableCharacter : MonoBehaviour
{
    public Team team;

    [SerializeField] private StatusText statusText;

    public PlayableCharacter(Team teamAssignment)
    {
        team = teamAssignment;
    }

    public Rigidbody Rigidbody { get; private set; }
    public bool CanJump { get; private set; }
    public StatusText StatusText => statusText;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        CanJump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        CanJump = collision.gameObject.CompareTag("Floor");
    }

    public void FlipCanJump()
    {
        CanJump = !CanJump;
    }
}

public enum Team
{
    Team1,
    Team2,
    Team3,
    Team4
}