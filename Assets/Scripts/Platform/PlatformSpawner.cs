using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // �÷��� ������
    public int poolSize = 5; // Ǯ�� ũ��
    public float spawnRate = 5f; // �÷����� �����Ǵ� ��
    public float platformLength = 30f; // �÷����� ����
    public Transform player; // �÷��̾��� Transform

    private Queue<GameObject> platformPool; // �÷��� Ǯ
    private Vector3 lastSpawnPoint; // ���������� ������ �÷����� ��ġ

    private void Start()
    {
        platformPool = new Queue<GameObject>();

        // ó���� Ǯ�� ũ�⸸ŭ �÷����� �����մϴ�.
        for (int i = 0; i < poolSize; i++)
        {
            GameObject platform = Instantiate(platformPrefab);
            platform.SetActive(false);
            platformPool.Enqueue(platform);
        }

        Debug.Log("Initial platform pool count: " + platformPool.Count);


        // ���� ���� �ʱ�ȭ
        lastSpawnPoint = transform.position;

        for (int i = 0; i < poolSize; i++)
        {
            SpawnPlatform();
        }
    }
       
    private void Update()
    {
     
        // �÷��̾ �ٰ����� ���ο� �÷����� �����ϰ� ������ �÷����� �����մϴ�.
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
