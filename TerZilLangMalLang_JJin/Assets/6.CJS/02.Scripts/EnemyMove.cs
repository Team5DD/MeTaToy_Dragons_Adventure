using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    int movespeed;
    //Animator anim;
    SpriteRenderer spriteRenderer;
    string enemyname;
    // Start is called before the first frame update
    void Awake()
    {
        if(this.gameObject.name.Contains("EnemyBee"))
        {
            movespeed = 3;
        }
        else if (this.gameObject.name.Contains("EnemyPlant"))
        {
            movespeed = 2;
        }
        if (this.gameObject.name.Contains("slug-1"))
        {
            movespeed = 1;
        }
        rigid = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        Invoke("Think", 2);

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
        nextMove = Random.Range(0,2) == 0 ? movespeed : -movespeed;
        //Sprite Animation
        //anim.SetInteger("WalkSpeed", nextMove);
        //Flip Sprite
        if (nextMove != 0)      
            spriteRenderer.flipX = nextMove == movespeed;
        //Recursive (재귀함수)
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == movespeed;

        CancelInvoke(); //모든 Invoke()를 멈추는 함수
        Invoke("Think", 1);
    }

   
}



