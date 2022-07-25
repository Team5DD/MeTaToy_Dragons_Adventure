using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HM_Typing_BirdMan : MonoBehaviour
{
    public TextMeshProUGUI txt_1;

    public GameObject txt1;

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
        message = "제군들,\n너희들이 붙잡아온 메타토이 드래곤 중에는 그가 없더군.\n그는 고고학자 출신으로 장난감 세상의 고대의 언어 뿐만 아니라 다른 장난감 언어에도 능통한 것으로 안다.\n아마 왕관의 조각을 합치는 방법 또한 알고 있겠지.\n그는 APE이라는 문자와 관련이 있는 것으로 파악됐다.\n이제부터 APE와 관련된 메타토이드래곤은 전부 잡아오도록.\n내가 하나씩 확인하도록 하지.\n\n내 원대한 여정에 함께하는 자는 왕좌에 오른 나의 곁에서 영원한 영광을 누리게 될 것이다. ";

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
 
    }
}
