using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Start()
    {
        
    }

    private void Update()
    {
        Vector3 dir = new Vector3(0, 6f, -7f);
        transform.position = target.position + dir;
    }
}
