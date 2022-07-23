using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed = 9f;
    public int getDamage_NB = 10;

    GameObject player;
    SpriteRenderer sprender;
    Rigidbody2D rigid;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sprender = player.GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        //rigid.AddForce(-player.transform.right * speed * Time.deltaTime, ForceMode2D.Force);   
    }

    void Update()
    {

        //this.transform.position += -player.transform.right * speed * Time.deltaTime;

        //this.transform.position += player.transform.right * speed * Time.deltaTime;


        if (sprender.flipX == false)
        {
            this.transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        }
        else
        {
            this.transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;

        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        }

}
