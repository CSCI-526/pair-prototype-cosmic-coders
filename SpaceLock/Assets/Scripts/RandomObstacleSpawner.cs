using UnityEngine;
using System.Collections.Generic;

public class RandomObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    private readonly float startDelay = 0f;
    private readonly float spawnInterval = 0.5f;
    private readonly int poolSize = 20;
    private List<GameObject> obstaclePool;
    private readonly float despawnX = 60f;

    void Start()
    {
        obstaclePool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
            GameObject obstacle = Instantiate(obstaclePrefabs[obstacleIndex]);
            obstacle.SetActive(false);
            obstaclePool.Add(obstacle);
        }

        InvokeRepeating(nameof(SpawnObstacles), startDelay, spawnInterval);
    }

    void SpawnObstacles() {
        GameObject obstacle = GetPooledObstacle();
        if (obstacle != null)
        {
            float randomZ = Random.Range(-32f, 18f);
            float x = -60f;
            float randomY = Random.Range(2f, 25f);

            Vector3 spawnPosition = new Vector3(x, randomY, randomZ);
            obstacle.transform.position = spawnPosition;
            obstacle.transform.rotation = obstaclePrefabs[0].transform.rotation;
            obstacle.SetActive(true);

            float randomScale = Random.Range(4f, 10f);
            obstacle.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            float mass = randomScale * 10f;

            if (obstacle.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.mass = mass;
            }
        }
    }

    GameObject GetPooledObstacle() {
        foreach (var obstacle in obstaclePool)
        {
            if (!obstacle.activeInHierarchy)
            {
                return obstacle;
            }
        }

        return null;
    }

    void Update() {
        foreach (var obstacle in obstaclePool)
        {
            if (obstacle.activeInHierarchy && obstacle.transform.position.x > despawnX)
            {
                RecycleObstacle(obstacle);
            }
        }
    }

    void RecycleObstacle(GameObject obstacle) {
        obstacle.SetActive(false);
    }
}
