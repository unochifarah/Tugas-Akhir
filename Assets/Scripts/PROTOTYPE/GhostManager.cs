using UnityEngine;
using System.Collections;

public class GhostManager : MonoBehaviour
{
    public GameObject[] ghostPrefabs; // Array of ghost prefabs
    public Transform[] spawnPoints; // Array of spawn points
    public float minSpawnTime = 5f; // Minimum time between spawns
    public float maxSpawnTime = 10f; // Maximum time between spawns

    private GameObject currentGhost;

    void Start()
    {
        StartCoroutine(SpawnGhosts());
    }

    IEnumerator SpawnGhosts()
    {
        while (true)
        {
            // Wait for a random amount of time before spawning the next ghost
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            // If there is already a ghost, continue to the next iteration
            if (currentGhost != null)
            {
                continue;
            }

            // Randomly select a ghost type and spawn point
            int ghostIndex = Random.Range(0, ghostPrefabs.Length);
            int spawnIndex = Random.Range(0, spawnPoints.Length);

            // Instantiate the ghost at the selected spawn point
            currentGhost = Instantiate(ghostPrefabs[ghostIndex], spawnPoints[spawnIndex].position, Quaternion.identity);
        }
    }

    public void GhostDisappeared()
    {
        currentGhost = null;
    }
}
