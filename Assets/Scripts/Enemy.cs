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
    
    public Transform target; // B 오브젝트 (따라갈 대상)

    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 sponPos = new Vector3(Random.Range(-2, 2), transform.position.y, transform.position.z + 10);
        initialPosition = sponPos;
        

        ChasePlayer();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ReturnToPool();
            
        }

        if (other.gameObject.tag == "DeadZone")
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
        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;
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
