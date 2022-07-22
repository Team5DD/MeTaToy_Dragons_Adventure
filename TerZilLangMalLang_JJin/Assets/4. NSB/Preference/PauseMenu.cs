using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    // 다른 스크립트에서 쉽게 접근이 가능하도록 static
    public static bool IsPaused = false;
    public GameObject PauseMenuCanvas;

    public AudioClip PlayBgm;
    public AudioClip EffectSound;

    public GameObject OffButton;
    public GameObject OnButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public void Resume()
    {   if (IsPaused)
        {
            PauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
            IsPaused = false;
        }
    }

    public void Pause()
    {   if (!IsPaused)
        {
            PauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
            IsPaused = true;
        }
    }
    public void Home()
    {
        SceneManager.LoadScene("Home");
    }
    
    
    void SoundOff()
    {
        OffButton.SetActive(false);
        OnButton.SetActive(true);
        
    }

    void SoundOn()
    {
        OnButton.SetActive(false);
    }
}
