using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public GameObject[] platforms; //플랫폼 프리팹

    //플랫폼 전체를 담는 부모 오브젝트
    GameObject newPlatform;

    //Player가 지나가면 삭제할 예전의 플랫폼
    GameObject oldPlatform;

    //newPlatform의 하위 오브젝트
    GameObject childPlatform;

    //플랫폼 종류 설정 변수
    GameObject platform;


    // Start is called before the first frame update
    void Start()
    {
        newPlatform = GameObject.Find("StartPlatform");
        oldPlatform = GameObject.Find("OldPlatform");
        childPlatform = newPlatform; //자식의 위치를 부모와 일치시킴

        MakePlatform(); // 플랫폼 만들기
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //플랫폼 생성하는 함수
    void MakePlatform()
    {
        DeleteOldPlatform(); //플레이어가 지나간 플랫폼
        MakeNewPlatform();
        

        
    }

    //플레이어가 지나간 플랫폼 삭제, 새로운 플랫폼 시작점 만들기.
    void DeleteOldPlatform()
    {
        Destroy(oldPlatform); //예전 플랫폼 삭제
        oldPlatform = newPlatform; //현재 플랫폼 저장

        //플랫폼 시작점 만들기
        newPlatform = new GameObject("StartPlatform"); //스테이지에 있는 StartPlatform을 새로만들어 newPlatform에 저장


    }

    //새로운 플랫폼 만들기
    //플랫폼은 한번에 3개씩 생성

    void MakeNewPlatform()
    {
        for(int i=0;i<3;i++)
        {
            platform = platforms[0]; //기본 플랫폼

            SelectPlatform(i);

            Vector3 pos = Vector3.zero; //플랫폼의 포지션

            Vector3 localPos = childPlatform.transform.localPosition; //플랫폼의 로컬포지션 (맨 마지막으로 만들어진 플랫폼의 로컬포지션으로 이위치를 기준으로 새로운 플랫폼 생성)

            pos = new Vector3(localPos.x, 0, localPos.z + 30);

            //새로운 플랫폼 만들고 부모 설정
            childPlatform = Instantiate(platform, pos,transform.rotation) as GameObject;
        }
    }


    //플랫폼 종류 설정하기
    void SelectPlatform(int n)
    {
        switch(n)
        {
            case 0:
            case 1:
            case 2:
                platform = platforms[Random.Range(0, platforms.Length)];
                break;
        }
    }
}
