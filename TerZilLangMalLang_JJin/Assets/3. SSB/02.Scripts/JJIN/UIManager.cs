using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject SuccessUI;
    public GameObject GetCrowUI;
    //public ParticleSystem succesEfx;    //성공시 나오는 파티클

    // Start is called before the first frame update
    void Start()
    {
        SuccessUI.SetActive(false);
        GetCrowUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
