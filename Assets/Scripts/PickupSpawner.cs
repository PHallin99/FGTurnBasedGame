using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private HealthPickUp[] healthPickUps;
    private readonly Stack<HealthPickUp> pickUpsToSpawn = new Stack<HealthPickUp>();

    private void Update()
    {
        if (pickUpsToSpawn.Count > 0)
        {
            SpawnPickUp();
        }
    }

    private void SpawnPickUp()
    {
        var healthPickUp = pickUpsToSpawn.Pop();
        healthPickUp.GetVisuals().SetActive(true);
        var transformPosition = transform.position;
        healthPickUp.transform.position = new Vector3(Random.Range(transformPosition.x, transformPosition.x + 32),
            transformPosition.y, Random.Range(transformPosition.z, transformPosition.z + 28));
    }

    public void PushPickUp(HealthPickUp pickUp)
    {
        pickUpsToSpawn.Push(pickUp);
    }

    public void PickUp(HealthPickUp pickUp)
    {
        pickUp.GetVisuals().SetActive(false);
        pickUp.transform.position = pickUp.SpawnPosition;
        pickUp.transform.rotation = Quaternion.Euler(Vector3.zero);
        pickUpsToSpawn.Push(pickUp);
        SpawnPickUp();
    }
}