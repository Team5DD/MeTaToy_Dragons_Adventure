using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb2 : MonoBehaviour
{
    public float speed = 9f;
    public int getDamage_NB = 10;



    SpriteRenderer BombSR;

    int xDir;

    private void Awake()
    {
        BombSR = this.GetComponent<SpriteRenderer>();
    }


    void Start()
    {
       
        Invoke("DestroyBomb", 2f);
    }

    void Update()
    {
      
        transform.Translate(Vector3.right * xDir * (speed * Time.deltaTime));
    }

    public void SetDirection(bool isRight)
    {
        BombSR.flipX = !isRight;
        xDir = isRight ? 1 : -1;
    }



    void DestroyBomb()
    {
        Destroy(gameObject);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }


}
