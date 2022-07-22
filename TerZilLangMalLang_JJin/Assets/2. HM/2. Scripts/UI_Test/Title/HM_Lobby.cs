using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HM_Lobby : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    //Animator anim;
    SpriteRenderer spriteRenderer;

    

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 2);
        StartCoroutine(ChatMessage());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 5, LayerMask.GetMask("Floor"));

        if (rayHit.collider == null)
            Turn();
    }

    //재귀함수 : 자신을 스스로 호출하는 함수
    void Think()
    {
        //Set Next Active
        nextMove = Random.Range(-1, 2);
        //Sprite Animation
        //anim.SetInteger("WalkSpeed", nextMove);

        //Flip Sprite
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == -1;
        //Recursive (재귀함수)
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == -1;

        CancelInvoke(); //모든 Invoke()를 멈추는 함수
        Invoke("Think", 2);
    }

    IEnumerator ChatMessage()
    {
        float a = Random.Range(3, 10);
        yield return new WaitForSeconds(a);
        

        yield return new WaitForSeconds(2f);
        
        StartCoroutine(ChatMessage());
    }
}
