using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_PlayerChoice : MonoBehaviour
{
    public static Save_PlayerChoice instance;

    public Sprite[] char_Prefeb;
    public Sprite char_Prefeb_Choice;
    public GameObject save_char_Img;
    public string save_char_Name;
    public int save_nft_Number;
    public int save_StageClear;

    public string TicketRandNum;

    // 스테이지 클리어 후 캐릭터 해금했을 때
    public bool isClear_1 = false;
    public bool isClear_2 = false;
    public bool isClear_3 = false;
    public bool isClear_4 = false;
    public bool isClear_5 = false;

    public bool isTitleSkip = false;


    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(this);

    }
}
