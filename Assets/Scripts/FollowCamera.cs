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
        targetFollow();
    }

    // 타겟을 0, 6f, -7 거리를 두고 따라가는 카메라기능
    private void targetFollow()
    {
        Vector3 dir = new Vector3(0, 6f, -7f);
        transform.position = target.position + dir;
    }
}
