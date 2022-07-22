using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HM_Typing_Word : MonoBehaviour
{
    public TextMeshProUGUI txt_1;

    public GameObject txt1;
    public GameObject txt2;

    AudioSource typingaudio;

     string message;
     string temp_message;
    public float speed = 0.2f;

    //public GameObject anyKey;

    // Start is called before the first frame update
    void Start()
    {
        typingaudio = GetComponent<AudioSource>();

        txt_1.text = "";
        message = "평화로운 장난감 세상에서 개성 넘치는 메타 토이 드래곤들이 살아가고 있었다.\n그들은 각자의 능력을 이용해, 왕관의 주인이 되어 장난감 세상의 왕좌에 오르기 위해 노력하고 있었다.";

        StartCoroutine(TypingAction());
    }

    IEnumerator TypingAction()
    {
        typingaudio.Play();
        for(int i =0; i<message.Length; i++)
        {
            yield return new WaitForSeconds(0.05f);

            temp_message += message.Substring(0, i);
            txt_1.text = temp_message;
            temp_message = "";
        }
        typingaudio.Stop();
        yield return new WaitForSeconds(1f);

        txt1.SetActive(false);
        txt2.SetActive(true);

        //anyKey.SetActive(true);
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                txt1.SetActive(false);
                txt2.SetActive(true);
            }
        }
    }
}
