using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int maxEnemyCount;
    int currentEnemyCount = 0;
    public GameObject[] Enemy;
    GameObject[] Enemy2;
    Transform[] SpawnPoint;
    void EnemyRandomSpawn()
    {
        int enemycount = 0;
        while(currentEnemyCount != maxEnemyCount)
        {
                int a = Random.Range(1, SpawnPoint.Length);
            if (SpawnPoint[a].GetComponent<SpawnPointBool>().existChild == false)
            {
                GameObject enemy = Instantiate(Enemy2[enemycount]);
                enemy.transform.position = SpawnPoint[a].transform.position;
                enemy.transform.SetParent(SpawnPoint[a].transform);
                SpawnPoint[a].GetComponent<SpawnPointBool>().existChild = true;
                currentEnemyCount++;
                enemycount++;
            }
        }
    }

    void Start()
    {
        Enemy2 =  new GameObject[maxEnemyCount];
        for (int i = 0; i < maxEnemyCount; i++)
        {
            int randomEnemy = Random.Range(0, Enemy.Length);
            Enemy2[i] = Enemy[randomEnemy];
        }

        SpawnPoint = this.transform.GetComponentsInChildren<Transform>(); 

        StartCoroutine("EnemyRandomSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
