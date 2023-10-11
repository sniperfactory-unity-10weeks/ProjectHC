using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Grade = 0;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        MinusCheck();
        PlusCheck();
    }

    private void PlusCheck()
    {
        if (Grade >= 5)
        {
            Grade = 5;
        }
    }
    
    private void MinusCheck()
    {
        if (Grade <= 0)
        {
            Grade = 0;
        }
    }
}
