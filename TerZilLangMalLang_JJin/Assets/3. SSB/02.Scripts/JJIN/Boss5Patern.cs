using System;
using System.Collections;
using UnityEngine;


public class Boss5Patern : MonoBehaviour
{
    public static Boss5Patern instance;
    private void Awake()
    {
        instance = this;
    }
    Animator anim;
    public Animator EfxAnim;    //자식이 애니메이터
    public int getDamage = 10;
    public int fireTowernum = 4;
    public int fireRainfirenum = 3;
    public GameObject floorboomPoint;
    public State state;
    public enum State
    {
        Idle,
        Pattern,
        TakeDamage,
        Die,
        Chase,
    }

    public GameObject PlayerTarget;

    public float AttackDistance = 1.5f;
    public float FindDistance = 10.0f;
    public float ChaseDistance = 5f;

    float moveSpeed = 1f;
    float attackTime;
    float curTime;

    [SerializeField] int nextMove;
    public GameObject[] FireFactory;
    public Transform FirePosiotion;
    public float Distance;
    SpriteRenderer spriterenderer;
    public GameObject Capsule;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {

        PlayerTarget = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        UIManager.instance.GetCrowUI.SetActive(false);
        UIManager.instance.SuccessUI.SetActive(false);
        Distance = PlayerTarget.transform.position.x - transform.position.x;
        Capsule.SetActive(false);
        spriterenderer = this.gameObject.GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        // Distance = PlayerTarget.transform.position.x - transform.position.x;
        //if (Mathf.Abs(Distance) <= Mathf.Abs(FindDistance))
        //{        
        //    MoveToTarget();
        //    FaceTarget();
        //    state = State.Move;
        //    if (Mathf.Abs(Distance) <= Mathf.Abs(AttackDistance))
        //    {
        //        state = State.Pattern;
        //    }
        //}
        FaceTarget();
        if (BossHP.instance.HP <= 0)
        {
            state = State.Die;
        }

        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Pattern:
                BossAI();
                break;
            case State.TakeDamage:
                TakeDamage();
                break;
            case State.Die:
                Die();
                break;

            case State.Chase:
                StartCoroutine("WaitPattern");
                break;

        }
    }
    bool p = false;
    IEnumerator WaitPattern()
    {
        if (p == false)
        {
            p = true;
            yield return new WaitForSeconds(3);
            state = State.Pattern;
        }
    }
    /*void MoveToTarget()
    {
        if (Mathf.Abs(Distance) <= Mathf.Abs(FindDistance))
        {
            float dir = PlayerTarget.transform.position.x - transform.position.x;
            dir = (dir < AttackDistance) ? -1 : 1;
            transform.Translate(new Vector2(dir, 0) * moveSpeed * Time.deltaTime);
        }
    }*/
    void FaceTarget()
    {
        if (PlayerTarget.transform.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            transform.localScale = new Vector3(-9, 9, 1);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(9, 9, 1);
        }
    }
    private void Idle()
    {
        anim.SetTrigger("Idle");
        EfxAnim.SetTrigger("IdleEfx");
        print("Idle");
        Distance = PlayerTarget.transform.position.x - transform.position.x;
        if (Mathf.Abs(Distance) <= Mathf.Abs(AttackDistance))
        {
            state = State.Pattern;
        }
    }
    /*private void Move()
    {
        anim.SetTrigger("Move");
        print("Move");
        Distance = PlayerTarget.transform.position.x - transform.position.x;
        if (Mathf.Abs(Distance) <= Mathf.Abs(FindDistance))
        {
            MoveToTarget();
            state = State.Move;
            if (Mathf.Abs(Distance) <= Mathf.Abs(AttackDistance))
            {
                state = State.Pattern;
            }
        }
    }*/
    //보스는 움직이지 않는다...
    bool die = false;
    private void Die()
    {
        if (die == false)
        {
            die = true;
            StartCoroutine("IEDie");
        }
    }
    ///죽음
    IEnumerator IEDie()
    {
        StartCoroutine(Blink(3));
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(1f);
        print("Die");
        this.gameObject.SetActive(false);
        GameObject capsule = Instantiate(Capsule);
        capsule.SetActive(true);
        capsule.transform.position = this.gameObject.transform.position;
        // StopAllCoroutines 주의 !!!
        StopAllCoroutines();

    }
    public void TakeDamage()
    {
        //색깔 바꾸기
        StartCoroutine("ColorChange");
        //데미지 얻기
        anim.SetTrigger("Hurt");
        Debug.Log("밤 맞고 데미지 얻음");
        BossHP.instance.HP -= getDamage;
        p = false;
        state = State.Chase;
        anim.SetTrigger("Idle");
    }
    private void BossAI()
    {
        anim.SetTrigger("Idle");
        //보스 상태가 공격 상태일때만 실행된다.
        print("BossAI");
        int randAction =0;
        switch (randAction)
        {
            case 0:
                //공격 0 패턴 - 불꽃비가 내려와~~~~
                BossFireRain();
                break;
            case 1:
                //공격 1 패턴 - 브레스 공격                 
                FireBreathe();
                break;

            case 2:
                FireTowerCount();
                //공격 2 패턴 - 불기둥 순차적으로

                break;

            case 3:
                //공격 3 패턴 - 불기둥 한번에
                
                break;
        }
    }
    /*private void BossTelePort()
    {
        StartCoroutine("BossTel");
        p = false;
        state = State.Chase;
    }
    IEnumerator BossTel()
    {
        StartCoroutine(Blink(2));
        yield return new WaitForSeconds(0.5f);
        this.transform.position = PlayerTarget.transform.position + new Vector3(0, 3, 0);
        rb.bodyType = RigidbodyType2D.Kinematic;

        StartCoroutine(Blink(2));
        yield return new WaitForSeconds(0.3f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(new Vector3(0, -0.001f, 0), ForceMode2D.Impulse);
        EfxAnim.SetTrigger("ExposionEfx");
        anim.SetTrigger("JumpAttack");
    }*/
    IEnumerator Blink(float num = 2)
    {
        for (int i = 0; i < num; i++)
        {
            spriterenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriterenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    /*몸통 박치기
    private void BossStrike()
    {
        FaceTarget();
        anim.SetTrigger("Strike");
        EfxAnim.SetTrigger("StrikeEfx");
        StartCoroutine("Dash");

        p = false;
        state = State.Chase;
    }
   
    IEnumerator Dash()
    {
        for (int i = 0; i < 100; i++)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, PlayerTarget.transform.position, 0.01f);
            yield return 0;
        }
    } */
    //보스 패턴 형식
    //불꽃 뿜기

    //불기둥 장판
    private void FireTowerCount()
    {
        FaceTarget();
        anim.SetTrigger("");
        EfxAnim.SetTrigger("");
        StartCoroutine("TowerCount");
        p = false;
        state = State.Chase;
    }
    IEnumerator TowerCount()
    {
        anim.SetTrigger("Jump");
        EfxAnim.SetTrigger("ExposionEfx");
        rb.AddForce(new Vector3(0, 0.001f, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.4f);
        rb.AddForce(new Vector3(0, -0.002f, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.4f);

        //StartCoroutine(Blink(3));
        yield return new WaitForSeconds(0.5f);
        GameObject Fire;
        for (int i = 0; i < fireTowernum; i++)
        {
            Fire = Instantiate(FireFactory[0]);
            //Fire.transform.position
        }
    }


    private void FireTowerOnce()
    {
        FaceTarget();

    }


    private void FireBreathe()
    {
        FaceTarget();
    }

   /* private void BossAttack()
    {
    FaceTarget();
    anim.SetTrigger("Attack");
    EfxAnim.SetTrigger("AttackEfx");

        GameObject[] Fire = new GameObject[bossAttackfirenum];
        for (int i=0; i < bossAttackfirenum; i++) {
          Fire[i] = Instantiate(FireFactory);
          Fire[i].transform.position = FirePosiotion.transform.position;
        }
        p = false;
        state = State.Chase;
    }*/


private void BossFireRain()
{
    StartCoroutine("FireRain");
    //점프 했다가 내려오면 불 내려오기
    p = false;
    state = State.Chase;
}

    IEnumerator FireRain()
    {
        anim.SetTrigger("Jump");
        EfxAnim.SetTrigger("ExposionEfx");
        rb.AddForce(new Vector3(0, 0.001f, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.4f);
        rb.AddForce(new Vector3(0, -0.002f, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(Blink(3));
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 10; i++)
        {
            GameObject Fire;
            yield return new WaitForSeconds(0.3f);
            Fire = Instantiate(FireFactory[0]);
            Fire.transform.position = floorboomPoint.transform.position + new Vector3(i * 3, 2, 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Bomb"))
    {
        if (BossHP.instance.HP > 1)
        {
                
                print("TakeDamage");
                state = State.TakeDamage;
                // TakeDamage();
                Destroy(collision.gameObject);
            }
            
        
    }
}
    IEnumerator ColorChange()
    {
        spriterenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriterenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriterenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriterenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
    }
}
