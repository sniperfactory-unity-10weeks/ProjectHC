using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float _speed =  20f;
    private GameManager gm;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        transform.Translate(new Vector3(0,0, _speed* Time.deltaTime));
        PlayerADMove();
        PlayMoveLimit();

    }
    
    // Player 좌우 이동 transform 으로 강제이동
    private void PlayerADMove()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-_speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
        }
    }
    
    // player 가 transform 으로 강제이동하기에 Limit 를 두어 좌우 Wall 을 넘지않게 제한
    private void PlayMoveLimit()
    {
        Vector3 posCheck = transform.position;
        posCheck.x = Mathf.Clamp(posCheck.x, -3.167f, 3.78f);
        transform.position = posCheck;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Upgrade")
        {
            gm.Grade += 1;
        }

        if (other.gameObject.tag == "Downgrade")
        {
            gm.Grade -= 1;
        }
    }
}
