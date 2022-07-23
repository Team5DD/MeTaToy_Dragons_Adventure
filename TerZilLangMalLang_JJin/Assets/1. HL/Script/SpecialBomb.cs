using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBomb : MonoBehaviour
{
    public float speed = 9f;
    public int getDamage_SB = 25;
    void Start()
    {
    }

    void Update()
    {
        this.transform.position += -transform.right * speed * Time.deltaTime;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
