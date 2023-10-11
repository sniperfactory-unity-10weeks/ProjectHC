using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 1;
    public Text scoreText;
    void Start()
    {
        
    }

    void Update()
    {
        scoreText.text = "현재점수 : " + score;
        Upgrade();
    }

    private void Upgrade()
    {
        switch (score)
        {
            case 10:
                Debug.Log("1");
                // 1단계 진화
                break;
            case 20:
                Debug.Log("2");
                // 2단계 진화
                break;
            case 30:
                Debug.Log("3");
                // 3단계 진화
                break;
            case 40:
                Debug.Log("4");
                // 4단계 진화
                break;
            case 50:
                Debug.Log("5");
                // 5단계 진화
                break;
        }
    }
}
