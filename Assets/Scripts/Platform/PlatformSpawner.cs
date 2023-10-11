using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public List<GameObject> platformPrefabs; // 플랫폼 프리팹들의 목록
    public int poolSize = 5; // 풀의 크기
    public float spawnRate = 5f; // 플랫폼이 생성되는 빈도
    public float platformLength = 30f; // 플랫폼의 길이
    public Transform player; // 플레이어의 Transform

    private Queue<GameObject> platformPool; // 플랫폼 풀
    public Vector3 lastSpawnPoint; // 마지막으로 생성된 플랫폼의 위치

    private void Start()
    {
        platformPool = new Queue<GameObject>();

        // 처음에 풀의 크기만큼 플랫폼을 생성합니다.
        for (int i = 0; i < poolSize; i++)
        {
            GameObject platform = Instantiate(GetRandomPlatformPrefab());
            platform.SetActive(false);
            platformPool.Enqueue(platform);
        }

        // 시작 지점 초기화
        lastSpawnPoint = transform.position;
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
        lastSpawnPoint = new Vector3(lastSpawnPoint.x, lastSpawnPoint.y, lastSpawnPoint.z + platformLength);
    }

    private void RecyclePlatform()
    {
        foreach (var platform in GameObject.FindGameObjectsWithTag("Platform"))
        {
            if (platform.activeSelf && (player.position.z - platform.transform.position.z) > platformLength)
            {
                platform.SetActive(false);
                platformPool.Enqueue(platform);
                return;
            }
        }
    }

    // 랜덤한 플랫폼 프리팹을 반환하는 함수
    private GameObject GetRandomPlatformPrefab()
    {
        return platformPrefabs[Random.Range(0, platformPrefabs.Count)];
    }
}
