using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HM_Fade_Manager : MonoBehaviour
{
    public Image image;
    public GameObject FadeUI;

    // Start is called before the first frame update
    void Start()
    {
        FadeUI.SetActive(true);
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeOut()
    {
        float fadeCount = 1;
        while (fadeCount > 0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }

        yield return new WaitForSeconds(1.0f);
        Destroy(FadeUI);
        Destroy(this);
    }
}
