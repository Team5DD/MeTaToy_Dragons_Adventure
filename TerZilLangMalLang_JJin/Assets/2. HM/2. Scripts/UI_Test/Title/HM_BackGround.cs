using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HM_BackGround : MonoBehaviour
{
    public Sprite[] backGrounds;
    public GameObject backGroundObject;
    public SpriteRenderer meshrender;
    AutoSave save;

    // Start is called before the first frame update
    void Start()
    {
        meshrender = backGroundObject.GetComponent<SpriteRenderer>();
        save = GameObject.Find("Player_Choice_Save(Clone)").GetComponent<AutoSave>();
    }

    // Update is called once per frame
    void Update()
    {
        if(save.gameData.isClear_1 == false)
        {
            meshrender.sprite = backGrounds[0];
        }
        if (save.gameData.isClear_1 == true)
        {
            meshrender.sprite = backGrounds[1];
        }
        if (save.gameData.isClear_2 == true)
        {
            meshrender.sprite = backGrounds[2];
        }
        if (save.gameData.isClear_3 == true)
        {
            meshrender.sprite = backGrounds[3];
        }
        if (save.gameData.isClear_4 == true)
        {
            meshrender.sprite = backGrounds[4];
        }
    }
}
