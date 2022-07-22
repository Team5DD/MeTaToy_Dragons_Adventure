using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//일정시간마다 스폰 포인트에서 두더지가 생기게 하고 싶다

public class moleManager_HL : MonoBehaviour
{
    public static moleManager_HL instance;
    private void Awake()
    {
        instance = this;
    }

    //두더지를 랜덤하게 발생시킴
    //나타나는 스폰 위치는 랜덤하다
    public Transform[] spawnList;
    //public Transform sqawnPosition;
    GameObject[] moleFactory; //mole prefab 장착

    //일정 시간이 지나면
    public float createTime = 1;
    float currentTime;

    // 생성시간을 랜덤하게 하고싶다.
    // 최소시간, 최대시간
    public float minTime = 0.5f;
    public float maxTime = 2;
    int prevIndex;

    GameObject[] moles;

    public GameObject mole;
    public GameObject targetMole;

    //두더지가 있는 곳  => moleFactory => ok
    // 총 20마리
    // 일정시간마다 두더지공장에서 두더지을 만들고 싶다


    //------moleCount----------------------------


    // 생성할 두더지의 수
    public int createMaxCount = 20; // 20마리
    // 생성한 두더지의 수
    public int createCount;
    // 파괴한 두더지의 수
    public int killCount;
    // 파괴한 두더지의 총 수
    public int totalKillCount = 5; // 5 마리
    
    //-------------moleCount----------------------------

    void Start()
    {
        //// 3초동안 기다렸다가 만들기 시작하고싶다.
        //yield return new WaitForSeconds(3);
        // Random.Range(0, 4);

        moleFactory = new GameObject[createMaxCount];
        moles = new GameObject[createMaxCount];

        for (int i = 0; i < 15; i++)
        {
            moleFactory[i] = mole;
        }
        for (int i = 0; i < 5; i++)
        {
            moleFactory[i + 15] = targetMole;
        }

        System.Random rng = new System.Random();
        List<int> sIndexes = new List<int>();

        for (int i = 0; i < createMaxCount; i++)
        {
            sIndexes.Add(i);
        }

        int n = createMaxCount;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (sIndexes[n], sIndexes[k]) = (sIndexes[k], sIndexes[n]);
        }


        for (int i = 0; i < createMaxCount; i++)
        {

            GameObject randMole = Instantiate(moleFactory[sIndexes[i]]);
            moles[i] = randMole;
            // 만약 생성수가 최대생성수보다 작다면
            if (createCount < createMaxCount)
            {
                createCount++;
                //GameObject mole = Instantiate(moleFactory);

                // 임의의 Spawn목록중 한곳
                // 1. 임의의 인덱스값을 결정하고싶다.
/*
                int index = Random.Range(0, spawnList.Length);
                // 만약 index가 직전에 선택한 index라면
                if (prevIndex == index)
                {
                    // 다른값으로 선택하고싶다.
                    index = (index + 1) % spawnList.Length;
                }
                prevIndex = index;
*/
                // 2. Spawn목록의 그 인덱스 위치값을 target으로 하고싶다.
                randMole.transform.position = spawnList[i].position;
                randMole.transform.rotation = spawnList[i].GetChild(0).rotation;
/*
                MOLEMove.instance.transform.position = spawnList[i].position;
                MOLEMove.instance.transform.rotation = spawnList[i].GetChild(0).rotation;
*/
            }
        }

        InvokeRepeating(nameof(ChangeMove), 0.0f, 2.0f);
    }

    //--------------------------------------------------------------

    // 두더지 위치 섞기
    void ChangeMove()
    {
        System.Random rng = new System.Random();  

        int n = moles.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (moles[n], moles[k]) = (moles[k], moles[n]);
        }
/*
        for (int i = 0; i < createMaxCount; i++)
        {
            var moleMove = moles[i].GetComponent<MOLEMove>();
            moleMove.startPosition = spawnList[i].position;
            moleMove.moveVector = spawnList[i].GetChild(0).forward;
            moleMove.gameObject.transform.up = spawnList[i].GetChild(0).forward;
            print(moles[i]);
        }
*/
    }

    void CountMole()
    {

    }
    //moleCount스크립트 같이??
    public Vector3 dir;
    void Update()
    {
        RandomMole();
        CountMole();
    }

    void RandomMole()
    {
 
    }
}
