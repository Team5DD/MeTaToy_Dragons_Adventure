using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public static Fire instance;
    private void Awake()
    {
        instance = this;

        rigid = GetComponent<Rigidbody2D>();
       // StartCoroutine(GainPowerTimer());
       //StartCoroutine(GainPower());
    }

    Rigidbody2D rigid;
    float angularPower = 2;
    float scaleValue = 0.1f;
    bool isShot;
    float fireyRange;
    public float myTime = 5f;
    float curTime;
    public GameObject PlayerTarget;

    // Start is called before the first frame update
    void Start()
    {
        fireyRange = Random.Range(-0.3f, 0.3f);
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        this.transform.localScale = new Vector3(1, 1, 1);
        this.transform.right = (PlayerTarget.transform.position - this.transform.position).normalized;
        
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (myTime < curTime)
        {
            Destroy(gameObject);
        }
        this.transform.localPosition += (this.transform.right+new Vector3(0, fireyRange, 0)) * 0.03f;
       // rigid.AddForce(Vector2.right * 0.01f, ForceMode2D.Impulse);
    }
/*
    void FaceTarget()
    {
        if (PlayerTarget.transform.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
*/
/*
    IEnumerator GainPowerTimer()
    {
        yield return new WaitForSeconds(2.2f);
        isShot = true;

    }
*/
/*
    IEnumerator GainPower()
    {
        FaceTarget();
        while (!isShot)
        {
            //원래 값
            //angularPower += 0.02f;
            //scaleValue += 0.005f;
            angularPower += 0.02f;
            scaleValue += 0.02f;
            //transform.localScale = Vector2.one;
            rigid.AddForce(Vector2.right * 0.5f, ForceMode2D.Impulse);

            yield return new WaitForSeconds(1f);
           // Destroy(this.gameObject);

        }

    }
*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

}
