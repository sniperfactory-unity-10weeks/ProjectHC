using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float speed = 30.0f; 
    private Vector3 initialPosition; // 몬스터의 초기 위치

    private void Start()
    {

    }

    private void Update()
    {
        Vector3 sponPos = new Vector3(Random.Range(-2, 2), transform.position.y, transform.position.z);
        initialPosition = sponPos;
        

        ChasePlayer();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ReturnToPool();
        }

        if (other.gameObject.tag == "Bullet")
        {
            ReturnToPool();
            other.gameObject.SetActive(false);
        }
    }

    private void ChasePlayer()
    {
        Vector3 direction = player.transform.position - transform.position;   // 플레이어를 향해 이동하는 방향 벡터 계산
               
        direction.Normalize();      // 항상 같은 속도로 추적하게 함
            
        transform.Translate(direction * speed * Time.deltaTime);    // 적을 플레이어 방향으로 이동
    }

    private void ReturnToPool()
    {
        // 초기 위치로 돌아가고 비활성화
        transform.position = initialPosition;
        gameObject.SetActive(false);

        // 풀로 반환
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.AddToMonsterPool(this.gameObject);
        }
    }
}
