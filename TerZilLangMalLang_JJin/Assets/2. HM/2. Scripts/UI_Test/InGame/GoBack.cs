using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBack : MonoBehaviour
{
    GameObject save;
    // Start is called before the first frame update
    void Start()
    {
        save = GameObject.Find("Player_Choice_Save");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GobackLobby()
    {
        SceneManager.LoadScene(1);
        Destroy(save);
    }
}
