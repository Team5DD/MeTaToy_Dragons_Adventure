using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwanplayer : MonoBehaviour
{


    GameObject playerchar;
    AutoSave savePlayer;

    GameObject playerspwan;
    public GameObject spwanpoint;

    public GameObject bossSpawnPoint;
    public GameObject bossSpawnPoint2;
    public GameObject[] boss;

    private void Awake()
    {
        playerchar = GameObject.Find("Player_Choice_Save(Clone)");
        savePlayer = playerchar.GetComponent<AutoSave>();
        playerspwan = AutoSave.instance.char_Prefeb_Choice;

        //spriteRender.sprite = playerspwan;

        GameObject player = Instantiate(playerspwan);
        player.transform.position = spwanpoint.transform.position;


        if(AutoSave.instance.gameData.isClear_4 == true)
        {
            GameObject boss1 = Instantiate(boss[3]);
            GameObject boss2 = Instantiate(boss[3]);
            boss1.transform.position = bossSpawnPoint.transform.position;
            boss2.transform.position = bossSpawnPoint2.transform.position;
        }
        else if (AutoSave.instance.gameData.isClear_3 == true)
        {
            GameObject boss1 = Instantiate(boss[3]);
            boss1.transform.position = bossSpawnPoint.transform.position;
        }
        else if (AutoSave.instance.gameData.isClear_2 == true)
        {
            GameObject boss1 = Instantiate(boss[2]);
            boss1.transform.position = bossSpawnPoint.transform.position;
        }
        else if (AutoSave.instance.gameData.isClear_1 == true)
        {
            GameObject boss1 = Instantiate(boss[1]);
            boss1.transform.position = bossSpawnPoint.transform.position;
        }
        else if (AutoSave.instance.gameData.isClear_1 == false)
        {
            GameObject boss1 = Instantiate(boss[0]);
            boss1.transform.position = bossSpawnPoint.transform.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}
