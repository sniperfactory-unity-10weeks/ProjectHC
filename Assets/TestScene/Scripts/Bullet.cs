using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private float _speed = 55f;

    private void Update()
    {
        Vector3 dir = Vector3.forward;
        transform.position += dir * _speed * Time.deltaTime;
    }
}
