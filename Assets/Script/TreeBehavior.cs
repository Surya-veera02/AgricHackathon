using UnityEngine;

public class TreeBehavior : MonoBehaviour
{
    public GameObject choppedLogPrefab;  // Prefab for the log to spawn
    public GameObject choppedBasePrefab; // Prefab for the tree base to spawn
    public Transform logSpawnPoint;      // Position to spawn the log
    public Transform baseSpawnPoint;     // Position to spawn the chopped base

    private bool isCut = false;

    public void OnCutDown()
    {
        if (isCut) return; // Prevent double-cutting
        isCut = true;

        // Spawn the chopped base
        if (choppedBasePrefab && baseSpawnPoint)
        {
            Instantiate(choppedBasePrefab, baseSpawnPoint.position, baseSpawnPoint.rotation);
        }

        // Spawn the log
        if (choppedLogPrefab && logSpawnPoint)
        {
            Instantiate(choppedLogPrefab, logSpawnPoint.position, logSpawnPoint.rotation);
        }

        // Destroy the main tree
        Destroy(gameObject);
    }
}
