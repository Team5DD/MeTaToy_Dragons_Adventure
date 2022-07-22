using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HM_TapToStart : MonoBehaviour
{
    
    
    public TextMeshProUGUI  anyBtnTxt;
    

    private void Start()
    {
        anyBtnTxt.GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeIn());
    }

    private void Update()
    {
       
    }

    IEnumerator FadeIn()
    {
        for (int i = 0; i < 15; i++)
        {
            float f = i / 15.0f;

            Color a = anyBtnTxt.color;
            a.a = f;
            anyBtnTxt.color = a;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        for (int i = 15; i >= 0; i--)
        {
            float f = i / 15.0f;
            Color a = anyBtnTxt.color;
            a.a = f;
            anyBtnTxt.color = a;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeIn());
    }
}
