using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private int poolSize = 10;
    private float fireRate = 1.0f; // 1초에 한 번 발사
    private List<GameObject> bulletObjectPool;
    private float lastFireTime = 0f;

    private void Start()
    {
        bulletObjectPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bulletObjectPool.Add(bullet);
            bullet.SetActive(false);
        }
    }

    private void Update()
    {
        // 시간이 지날 때마다 자동으로 발사
        if (Time.time - lastFireTime >= 0.5f / fireRate)
        {
            Fire();
            lastFireTime = Time.time;
        }
    }

    private void Fire()
    {
        // 오브젝트 풀에서 총알을 가져와 발사
        GameObject bullet = GetPooledBullet();
        if (bullet != null)
        {
            bullet.SetActive(true);
            bullet.transform.position = transform.position;
            StartCoroutine(ReturnBulletToPool(bullet, 1.5f)); // 1.5초 후에 다시 풀로 반환
        }
    }

    private GameObject GetPooledBullet()
    {
        foreach (var bullet in bulletObjectPool)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }
        return null; // 사용 가능한 총알이 없을 경우 null 반환
    }

    private IEnumerator ReturnBulletToPool(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        bullet.SetActive(false);
    }
}
