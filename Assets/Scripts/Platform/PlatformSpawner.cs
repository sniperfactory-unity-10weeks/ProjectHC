using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
     public GameObject[] platformPrefabs; // 다양한 플랫폼 프리팹을 저장할 배열
    public int poolSize = 6;
    public float spawnRate = 5f;
    public float platformLength = 30f;
    public Transform player;

    private Queue<GameObject> platformPool;
    private Vector3 lastSpawnPoint;

    private void Start()
    {
        platformPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject platformPrefab = GetRandomPlatformPrefab();
            GameObject platform = Instantiate(platformPrefab);
            platform.SetActive(false);
            platformPool.Enqueue(platform);
        }

        Debug.Log("Initial platform pool count: " + platformPool.Count);

        lastSpawnPoint = transform.position;

        for (int i = 0; i < poolSize; i++)
        {
            SpawnPlatform();
        }
    }

    private void Update()
    {
        if (player.position.z > lastSpawnPoint.z - (poolSize * platformLength) + platformLength)
        {
            SpawnPlatform();
            RecyclePlatform();
        }
    }

    private GameObject GetRandomPlatformPrefab()
    {
        int randomIndex = Random.Range(0, platformPrefabs.Length);
        return platformPrefabs[randomIndex];
    }

    private void SpawnPlatform()
    {
        if (platformPool.Count == 0)
        {
            Debug.Log("Platform pool is empty!");
            return;
        }

        GameObject platformPrefab = GetRandomPlatformPrefab();
        GameObject platform = platformPool.Dequeue();
        platform.transform.position = lastSpawnPoint;
        platform.SetActive(true);
        Debug.Log("Spawning platform at position: " + lastSpawnPoint.z + ". Pool count before spawn: " + platformPool.Count);

        lastSpawnPoint = new Vector3(lastSpawnPoint.x, lastSpawnPoint.y, lastSpawnPoint.z + platformLength);
    }

    private void RecyclePlatform()
    {
        foreach (var platform in GameObject.FindGameObjectsWithTag("Platform"))
        {
            Debug.Log("Platform Z: " + platform.transform.position.z + ", Player Z: " + player.position.z);

            if (platform.activeSelf && (player.position.z - platform.transform.position.z) > platformLength)
            {
                Debug.Log("Recycling platform at: " + platform.transform.position);
                platform.SetActive(false);
                platformPool.Enqueue(platform);
                return;
            }
        }
    }
}
