using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // 플랫폼 프리팹
    public int poolSize = 5; // 풀의 크기
    public float spawnRate = 5f; // 플랫폼이 생성되는 빈도
    public float platformLength = 30f; // 플랫폼의 길이
    public Transform player; // 플레이어의 Transform

    private Queue<GameObject> platformPool; // 플랫폼 풀
    private Vector3 lastSpawnPoint; // 마지막으로 생성된 플랫폼의 위치

    private void Start()
    {
        platformPool = new Queue<GameObject>();

        // 처음에 풀의 크기만큼 플랫폼을 생성합니다.
        for (int i = 0; i < poolSize; i++)
        {
            GameObject platform = Instantiate(platformPrefab);
            platform.SetActive(false);
            platformPool.Enqueue(platform);
        }

        Debug.Log("Initial platform pool count: " + platformPool.Count);


        // 시작 지점 초기화
        lastSpawnPoint = transform.position;

        for (int i = 0; i < poolSize; i++)
        {
            SpawnPlatform();
        }
    }
       
    private void Update()
    {
     
        // 플레이어가 다가오면 새로운 플랫폼을 생성하고 오래된 플랫폼을 재사용합니다.
        if (player.position.z > lastSpawnPoint.z - (poolSize * platformLength) + platformLength)
        {
            SpawnPlatform();
            RecyclePlatform();
        }
    }

    private void SpawnPlatform()
    {
        if (platformPool.Count == 0)
        {
            Debug.Log("Platform pool is empty!");
            return;
        }

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
