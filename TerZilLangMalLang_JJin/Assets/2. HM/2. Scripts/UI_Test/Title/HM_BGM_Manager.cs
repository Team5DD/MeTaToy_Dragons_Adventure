using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HM_BGM_Manager : MonoBehaviour
{
    public static HM_BGM_Manager instance;

   public AudioSource[] bgmSource;

    public bool isDestroy = false;

    Scene scene;
    string sceneName;

    string titleSecene = "MainTitle";
    string choiceSecne = "Char_Choice";
    string inGameSecene1 = "Stage1";
    string inGameSecene2 = "Stage2";
    string inGameSecene3 = "Stage3";
    string inGameSecene4 = "Stage4";
    string inGameSecene5 = "Stage5";

    private void Awake()
    {
        
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bgmSource[0].Play();
    }

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        if(sceneName == titleSecene) { TitleBGM(); }
        else if(sceneName == choiceSecne) { TitleBGM(); }
        else if(sceneName != titleSecene && sceneName != choiceSecne) { InGameBGM(); }

        if(isDestroy == false)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    void TitleBGM()
    {
        bgmSource[1].Stop();
        bgmSource[2].Stop();
        bgmSource[3].Stop();
        if (bgmSource[0].isPlaying == false)
        {
            bgmSource[0].Play();
        }
    }

    void InGameBGM()
    {
        if(PlayerController.instance.isMeetBoss == false)
        {
            bgmSource[0].Stop();
            bgmSource[2].Stop();

            if (bgmSource[1].isPlaying == false)
            {
                bgmSource[1].Play();
            }
        }
        else
        {
            bgmSource[0].Stop();
            bgmSource[1].Stop();
            bgmSource[2].Stop();
            if (bgmSource[3].isPlaying == false) { bgmSource[3].Play(); }
        }
        
    }

    void MeetBoss()
    {
        bgmSource[0].Stop();
        bgmSource[1].Stop();
        bgmSource[3].Stop();
        int a = Random.Range(0, 2);

        switch (a)
        {
            case 0:
                if(bgmSource[2].isPlaying == false) { bgmSource[2].Play(); }
                break;

            case 1:
                if (bgmSource[3].isPlaying == false) { bgmSource[3].Play(); }
                break;
        }
    }
}
