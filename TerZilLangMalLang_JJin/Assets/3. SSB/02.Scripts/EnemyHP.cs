using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public static EnemyHP instance;
    private void Awake()
    {
        instance = this;
    }
    SpriteRenderer sp;
    public int maxHP = 100;
    public Slider sliderEnemyHP;
    int hp;

    [SerializeField] int BombAttack=10;
    [SerializeField] int SBombAttack = 25;

    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            sliderEnemyHP.value = hp;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sp = this.GetComponent<SpriteRenderer>();
        sliderEnemyHP = this.gameObject.GetComponentInChildren<Slider>();
        sliderEnemyHP.maxValue = maxHP;
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator blink()
    {
        sp.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sp.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sp.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sp.color = Color.white;
        yield return new WaitForSeconds(0.1f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Bomb"))
        {
            StartCoroutine("blink");
            if (HP > 0)
            {
                HP -= BombAttack;
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("SBomb"))
        {

            StartCoroutine("blink");
            if (HP > 0)
            {
                HP -= SBombAttack;
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
