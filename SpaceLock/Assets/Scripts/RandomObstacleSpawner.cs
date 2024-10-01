using UnityEngine;
using System.Collections.Generic;

public class RandomObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private readonly float startDelay = 2f;
    private readonly float spawnInterval = 1.5f;
    private readonly int poolSize = 10;
    private List<GameObject> obstaclePool;
    private readonly float despawnX = 60f;

    void Start()
    {
        // Initialize the object pool
        obstaclePool = new List<GameObject>();

        // Populate the object pool
        for (int i = 0; i < poolSize; i++)
        {
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            GameObject obstacle = Instantiate(obstaclePrefabs[obstacleIndex]);
            obstacle.SetActive(false);  // Disable the object initially
            obstaclePool.Add(obstacle);
        }

        InvokeRepeating(nameof(SpawnObstacles), startDelay, spawnInterval);
    }

    void SpawnObstacles()
    {
        // Find an inactive object in the pool
        GameObject obstacle = GetPooledObstacle();
        if (obstacle != null)
        {
            float randomZ = Random.Range(-32f, 18f);
            float x = -30f;
            float randomY = Random.Range(2f, 25f);

            Vector3 spawnPosition = new Vector3(x, randomY, randomZ);
            obstacle.transform.position = spawnPosition;
            obstacle.transform.rotation = obstaclePrefabs[0].transform.rotation;
            obstacle.SetActive(true); // Activate the object

            // Scale and set mass
            float randomScale = Random.Range(2f, 10f);
            obstacle.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            float mass = randomScale * 10f;

            if (obstacle.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.mass = mass;
                Debug.Log("Obstacle Mass: " + mass);
            }
        }
    }

    GameObject GetPooledObstacle()
    {
        // Search for an inactive object in the pool
        foreach (var obstacle in obstaclePool)
        {
            if (!obstacle.activeInHierarchy)
            {
                return obstacle;
            }
        }

        return null;
    }

    void Update()
    {
        // Check for obstacles to recycle
        foreach (var obstacle in obstaclePool)
        {
            if (obstacle.activeInHierarchy && obstacle.transform.position.x > despawnX)
            {
                RecycleObstacle(obstacle);
            }
        }
    }

    void RecycleObstacle(GameObject obstacle)
    {
        // Deactivate and prepare the obstacle to be reused
        obstacle.SetActive(false);
    }
}
