using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    SpriteRenderer sp;
    GameObject sfxManager;
    AudioSource hit_sfx;
    AudioSource die_sfx;
    Rigidbody2D playerrig;

    public static PlayerHP instance;
    private void Awake()
    {
        instance = this;
    }

    Image[] playerHP;
    public int damagecount = 0;
    bool isHitCoolDown = false;
    void Start()
    {
        playerrig = GetComponent<Rigidbody2D>();
        playerHP = GameObject.Find("PlayerHP").GetComponentsInChildren<Image>();
        sp = this.gameObject.GetComponent<SpriteRenderer>();
        sfxManager = GameObject.Find("BGMManager");
        hit_sfx = sfxManager.transform.GetChild(4).GetComponent<AudioSource>();
        die_sfx = sfxManager.transform.GetChild(3).GetComponent<AudioSource>();
    }
    IEnumerator blink()
    {
       
        sp.color = Color.gray;
        yield return new WaitForSeconds(0.1f);
        sp.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sp.color = Color.gray;
        yield return new WaitForSeconds(0.1f);
        sp.color = Color.white;
        yield return new WaitForSeconds(1f);

        isHitCoolDown = false;
    }

    private void Update()
    {
        if (damagecount == 10)
        {
            die_sfx.Play();
            Time.timeScale = 0;
            CharacterOpen.instance.GameOverUI();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            print("PlayerHit");
            if(isHitCoolDown == false)
            {
                damagecount++;

                playerHP[damagecount - 1].gameObject.SetActive(false);

                int dirc = this.transform.position.x - collision.transform.position.x > 0 ? 1 : -1;

                playerrig.AddForce(new Vector2(dirc, 1) * 5, ForceMode2D.Impulse);
            }
            isHitCoolDown = true;

            if (!hit_sfx.isPlaying)
            {
                hit_sfx.Play();
            }

            StartCoroutine("blink");

                     
        }
    }
}
