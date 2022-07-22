using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HM_Typing_Word3 : MonoBehaviour
{
    public TextMeshProUGUI txt_1;

    public GameObject txt3;
    AudioSource typingaudio;
    string message;
    string temp_message;
    public float speed = 0.2f;

    public GameObject anyKey;

    // Start is called before the first frame update
    void Start()
    {
        typingaudio = GetComponent<AudioSource>();
        txt_1.text = "";
        message = "생기를 잃은 세상과 붙잡혀버린 친구들..\n장난감 세상을 사랑하는 원더 드래곤은 버드맨에게 붙잡힌 메타 토이 드래곤들을\n구출해내기로 결심해 모험을 떠난다.";

        StartCoroutine(TypingAction());
    }

    IEnumerator TypingAction()
    {
        typingaudio.Play();
        for (int i = 0; i < message.Length; i++)
        {
            yield return new WaitForSeconds(0.05f);

            temp_message += message.Substring(0, i);
            txt_1.text = temp_message;
            temp_message = "";
        }
        typingaudio.Stop();
        yield return new WaitForSeconds(1f);

        anyKey.SetActive(true);
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                this.gameObject.SetActive(false);
                anyKey.SetActive(true);
            }
        }
    }
}
