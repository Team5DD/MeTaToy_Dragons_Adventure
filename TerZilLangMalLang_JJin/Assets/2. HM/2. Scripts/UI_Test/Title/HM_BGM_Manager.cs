using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HM_BGM_Manager : MonoBehaviour
{
    public static HM_BGM_Manager instance;

    public bool isDestroy = false;

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
        if(isDestroy == false)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
}
