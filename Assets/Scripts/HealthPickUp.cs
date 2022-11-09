using Managers;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private PickupSpawner pickupSpawner;

    public Vector3 SpawnPosition { get; private set; }
    [SerializeField] private GameObject visuals;

    private void Start()
    {
        SpawnPosition = transform.position;
        pickupSpawner.PushPickUp(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Player")) return;
        collision.collider.GetComponent<PlayableCharacter>().GiveHealth(health);
        Game.TeamsTracker.UpdateHp();
        Respawn();
    }

    public void Respawn()
    {
        visuals.SetActive(false);
        pickupSpawner.PickUp(this);
    }

    public GameObject GetVisuals()
    {
        return visuals;
    }
}