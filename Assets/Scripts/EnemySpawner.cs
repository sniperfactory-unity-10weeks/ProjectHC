using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // 몬스터 프리팹
    public int poolSize = 5; // 몬스터 풀 크기
    public float spawnRate = 5f; // 몬스터 생성 간격
    public Transform player; // 플레이어의 Transform
    public float platformLength = 30f;
    private Queue<GameObject> monsterPool; // 몬스터 풀
    private Vector3 lastSpawnPoint; // 마지막 몬스터가 생성된 위치

    private void Start()
    {
        monsterPool = new Queue<GameObject>();

        // 몬스터 풀 초기화
        for (int i = 0; i < poolSize; i++)
        {
            GameObject monster = Instantiate(monsterPrefab);
            monster.SetActive(false);
            monsterPool.Enqueue(monster);
        }

        // 초기 몬스터 생성
        lastSpawnPoint = transform.position;
        for (int i = 0; i < poolSize; i++)
        {
            SpawnMonster();
        }
    }

    private void Update()
    {
        // 플레이어가 일정 거리 이동하면 몬스터 생성 및 재활용
        if (player.position.z > lastSpawnPoint.z - (poolSize * platformLength) + spawnRate)
        {
            SpawnMonster();
            RecycleMonster();
        }
    }

    private void SpawnMonster()
    {
        if (monsterPool.Count == 0)
        {
            Debug.Log("Monster pool is empty!");
            return;
        }

        GameObject monster = monsterPool.Dequeue();
        monster.transform.position = lastSpawnPoint;
        monster.SetActive(true);

        // 새로운 몬스터 생성 위치 설정
        lastSpawnPoint = new Vector3(lastSpawnPoint.x, lastSpawnPoint.y, lastSpawnPoint.z + spawnRate);
    }

    private void RecycleMonster()
    {
        foreach (var monster in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (monster.activeSelf && (player.position.z - monster.transform.position.z) > spawnRate)
            {
                monster.SetActive(false);
                monsterPool.Enqueue(monster);
                return;
            }
        }
    }
    
    public void AddToMonsterPool(GameObject monster)
    {
        // 몬스터를 비활성화하고 몬스터 풀에 추가
        monster.SetActive(false);
        monsterPool.Enqueue(monster);
    }

}
