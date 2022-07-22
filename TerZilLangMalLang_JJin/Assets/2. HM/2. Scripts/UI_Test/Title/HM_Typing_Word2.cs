using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HM_Typing_Word2 : MonoBehaviour
{
    public TextMeshProUGUI txt_1;

    public GameObject txt2;
    public GameObject txt3;
    AudioSource typingaudio;
    string message;
    string temp_message;
    public float speed = 0.2f;

    

    // Start is called before the first frame update
    void Start()
    {
        typingaudio = GetComponent<AudioSource>();
        txt_1.text = "";
        message = "하지만, 어느 날 알 수 없는 버그로 생긴 차원의 균열로 인해\n다른 게임 세상의 몬스터들이 장난감 세상에 나타나기 시작했다.\n강한 마력을 가지고 있는 버드맨은 이 틈을 타 장난감 세상에 그의 군단을 이끌고 공격을 가했다.\n그 충격으로 인해 장난감 세상의 균형을 유지하던 왕관이 조각나 장난감 세상 곳곳으로 흩어지게 되었고\n많은 메타 토이 드래곤들은 다치거나, 장난감 캡슐에 포획당해버렸다.";

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
        yield return new WaitForSeconds(2.5f);

        txt2.SetActive(false);
        txt3.SetActive(true);

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
                txt2.SetActive(false);
                txt3.SetActive(true);
            }
        }
    }
}
