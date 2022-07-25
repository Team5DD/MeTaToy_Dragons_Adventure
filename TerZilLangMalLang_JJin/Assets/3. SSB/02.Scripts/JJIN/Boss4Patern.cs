using System;
using System.Collections;
using UnityEngine;


public class Boss4Patern : MonoBehaviour
{
    public static Boss4Patern instance;
    private void Awake()
    {
        instance = this;
    }

    Animator anim;
    public Animator EfxAnim;    //자식이 애니메이터
    public int getDamage = 10;
    public int bossAttackfirenum = 4;
    public int fireRainfirenum = 5;
    GameObject capsule;
    Boss5_HP boss5_HP;
    int diecount;
    public State state;
    public enum State
    {
        Idle,

        Pattern,

        Move,
        TakeDamage,

        Die,

        Chase,


    }

    public GameObject PlayerTarget;
    Transform[] SpawnPosition;
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
        boss5_HP = this.GetComponent<Boss5_HP>();
        SpawnPosition = GameObject.Find("FlyKickSpawnPosition").transform.GetComponentsInChildren<Transform>();
        PlayerTarget = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        Distance = PlayerTarget.transform.position.x - transform.position.x;
        spriterenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    bool facetarget = true;
    void Update()
    {
        print(diecount);
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
        if (facetarget == true)
        {
            FaceTarget();
        }
        if (boss5_HP.HP <= 0)
        {

            state = State.Die;
        }

        switch (state)
        {
            case State.Idle:
                Idle();
                break;

            case State.Move:
                Move();
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
            yield return new WaitForSeconds(2);
            state = State.Pattern;
        }
    }
    void MoveToTarget()
    {
        if (Mathf.Abs(Distance) <= Mathf.Abs(FindDistance))
        {
            float dir = PlayerTarget.transform.position.x - transform.position.x;
            dir = (dir < AttackDistance) ? -1 : 1;
            transform.Translate(new Vector2(dir, 0) * moveSpeed * Time.deltaTime);
        }
    }
    void FaceTarget()
    {
        if (PlayerTarget.transform.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            transform.localScale = new Vector3(-2.5f, 2.5f, 1);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(2.5f, 2.5f, 1);
        }
        // anim.SetTrigger("Move");
    }
    private void Idle()
    {
        anim.SetTrigger("Idle");
        EfxAnim.SetTrigger("IdleEfx");

        print("Idle");
        Distance = PlayerTarget.transform.position.x - transform.position.x;
        if (Mathf.Abs(Distance) <= Mathf.Abs(FindDistance))
        {
            MoveToTarget();

            state = State.Move;
            /*
                        if (Mathf.Abs(Distance) <= Mathf.Abs(AttackDistance))
                        {
                            state = State.Pattern;
                        }
            */
        }
    }
    private void Move()
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
    }
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

        if (AutoSave.instance.gameData.isClear_4 == false)
        {
            capsule = Instantiate(Capsule);
            capsule.SetActive(true);
            capsule.transform.position = this.gameObject.transform.position;
        }

        // StopAllCoroutines 주의 !!!
        StopAllCoroutines();

    }
    //UIManager.instance.SuccessUI.SetActive(true);
    //StartCoroutine("OffSiccessUI");

    //UIManager.instance.SuccessUI.SetActive(false);
    //yield return new WaitForSeconds(1f);
    //Debug.Log("bbb");

    //UIManager.instance.GetCrowUI.SetActive(true);
    //yield return new WaitForSeconds(1f);
    //Debug.Log("ccc");
    //UIManager.instance.GetCrowUI.SetActive(false);
    //Destroy(this.gameObject);

    public void TakeDamage()
    {
        //색깔 바꾸기
        StartCoroutine("ColorChange");
        //데미지 얻기
        anim.SetTrigger("Hurt");
        Debug.Log("밤 맞고 데미지 얻음");
        boss5_HP.HP -= getDamage;
        anim.SetTrigger("Idle");
        state = State.Chase;
    }

    //public void FireShot()
    //{

    //    //Destroy(Fire);

    //    GameObject Fire = Instantiate(FireFactory);
    //    Fire.transform.position = FirePosiotion.transform.position;
    //    Fire.GetComponent<Rigidbody2D>().velocity = new Vector2(nextMove, 0);


    //    state = State.Idle;

    //}


    private void BossAI()
    {
        anim.SetTrigger("Idle");
        //보스 상태가 공격 상태일때만 실행된다.
        print("BossAI");
        int randAction = UnityEngine.Random.Range(0, 4);
        //int randAction = 0;

        switch (randAction)
        {

            case 0:
                //공격 0 패턴 - Blink 후 Player 기준 왼쪽에서 오른쪽 / 오른쪽에서 왼쪽 FlyKick
                FlyKick();
                break;
            case 1:
                //공격 1 패턴 - 불꽃 뿜기               
                BossAttack();
                break;

            case 2:
                //공격 2 패턴 - 몸통 박치기
                BossTelePort();
                break;

            case 3:
                //공격 3 패턴 - Player 머리위에서 일정 간격으로 불 내려오기
                BossFireRain();
                break;
        }
    }


    private void FlyKick()
    {
        StartCoroutine("Flykick");
        state = State.Chase;
        p = false;
    }

    IEnumerator Flykick()
    {
        facetarget = false;
        anim.SetBool("FlyKick", true);
        anim.SetTrigger("flykick");
        StartCoroutine(Blink(2));
        this.transform.position = SpawnPosition[1].transform.position;
        //rb.simulated = false;
        StartCoroutine(Blink(1));
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 100; i++)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, SpawnPosition[2].transform.position, 0.01f);
            yield return 0;
        }
        this.transform.position = SpawnPosition[3].transform.position;
        StartCoroutine(Blink(2));
        transform.localScale = new Vector3(-2.5f, 2.5f, 1);
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 100; i++)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, SpawnPosition[4].transform.position, 0.01f);
            yield return 0;
        }
        // rb.simulated = true;
        anim.SetBool("FlyKick", false);
        yield return new WaitForSeconds(0.3f);
        facetarget = true;
    }
    /*
        IEnumerator Dash()
        {

        }
    */
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
    //보스 패턴 형식
    //불꽃 뿜기
    private void BossAttack()
    {
        FaceTarget();

        p = false;
        state = State.Chase;
    }
    IEnumerator BA()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        EfxAnim.SetTrigger("AttackEfx");
        GameObject[] Fire = new GameObject[bossAttackfirenum];
        for (int i = 0; i < bossAttackfirenum; i++)
        {
            Fire[i] = Instantiate(FireFactory[0]);
            Fire[i].transform.position = FirePosiotion.transform.position;
        }
        yield return 0;
    }

    //몸통 박치기
    private void BossTelePort()
    {
        StartCoroutine("BossTel");
        p = false;
        state = State.Chase;
    }
    IEnumerator BossTel()
    {
        StartCoroutine(Blink(2));
        yield return new WaitForSeconds(0.5f);
        //this.transform.position = PlayerTarget.transform.position + new Vector3(0, PlayerTarget.transform.position.y +6, 0);
        this.transform.position = new Vector3(PlayerTarget.transform.position.x, PlayerTarget.transform.position.y + 6, 0);
        rb.bodyType = RigidbodyType2D.Kinematic;

        StartCoroutine(Blink(2));
        yield return new WaitForSeconds(0.3f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(new Vector3(0, -0.002f, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        GameObject[] Fire = new GameObject[bossAttackfirenum];
        int a = 1;
        for (int i = 0; i < 2; i++)
        {
            a = a * -1;
            Fire[i] = Instantiate(FireFactory[1]);
            Fire[i].transform.position = FirePosiotion.transform.position + new Vector3(0, -1f, 0);
            Fire[i].transform.right = FirePosiotion.transform.right * a;
        }
        EfxAnim.SetTrigger("ExposionEfx");
        anim.SetTrigger("JumpAttack");
    }




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
        rb.AddForce(new Vector3(0, 0.002f, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.4f);
        rb.AddForce(new Vector3(0, -0.003f, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(Blink(3));
        yield return new WaitForSeconds(0.5f);
        GameObject[] Fire = new GameObject[fireRainfirenum];
        for (int i = 0; i < fireRainfirenum; i++)
        {
            Fire[i] = Instantiate(FireFactory[0]);
            Fire[i].transform.localScale = new Vector3(3f, 3f, 3f);
            Fire[i].transform.position = PlayerTarget.transform.position + new Vector3(-(fireRainfirenum % 2) + i, 5f, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            if (boss5_HP.HP > 1)
            {
                print("TakeDamage");
                state = State.TakeDamage;
                // TakeDamage();
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.CompareTag("SBomb"))
            {
                if (boss5_HP.HP > 1)
                {

                    Destroy(collision.gameObject);
                    print("TakeDamage");
                    state = State.TakeDamage;
                    // TakeDamage();
                }
                else
                {
                    Destroy(collision.gameObject);
                }
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
