using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5_Capusle : MonoBehaviour
{
    GameObject[] boss5;
    Boss5_HP boss1;
    Boss5_HP boss2;

    public GameObject Capsuleprefeb;
    GameObject Capusle;

    // Start is called before the first frame update
    void Start()
    {
        boss5 = GameObject.FindGameObjectsWithTag("Boss5");
        boss1 = boss5[0].transform.GetChild(0).GetComponent<Boss5_HP>();
        boss2 = boss5[1].transform.GetChild(0).gameObject.GetComponent<Boss5_HP>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boss1.HP <= 0 && boss2.HP <= 0)
        {
            Capusle = Instantiate(Capsuleprefeb);
            Capusle.SetActive(true);
            Capusle.transform.position = boss5[1].transform.position;
        }
    }
}
