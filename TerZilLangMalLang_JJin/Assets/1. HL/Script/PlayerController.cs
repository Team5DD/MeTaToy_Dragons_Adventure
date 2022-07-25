using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
    }

    VirtualJoystick virtualJoystick;
    [SerializeField]
    private float moveSpeed = 7;
    public float jumpPower = 5f;
    Rigidbody2D rigid2d;

    public GameObject[] Enemy;
    [SerializeField] float JumpCount = 2;
    private bool IsJump;
    public bool isMeetBoss = false;
    bool isRight = true;

    public Camera camera;


    public void Start()
    {
        virtualJoystick = GameObject.Find("PlayerCanvas").transform.GetChild(0).GetComponent<VirtualJoystick>();
        rigid2d = GetComponent<Rigidbody2D>();
        JumpCount = 2;
    }

    private void Update()
    {
        float x = virtualJoystick.Horizontal;   // 좌/우 이동
                                                //float y = virtualJoystick.Vertical;		// 위/아래 이동

        if (x != 0)
        {
            transform.position += new Vector3(x, 0, 0) * moveSpeed * Time.deltaTime;
            isRight = x > 0 ? true : false;
        }
    }

    public void OnButtonDown()
    {
        IsJump = true;
        if (IsJump == true)
        {
            if (JumpCount > 0.0f)
            {
                JumpCount--;
                //rigid2d.velocity = new Vector2(rigid2d.velocity.y, jumpPower);

                rigid2d.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            }
        }
    }


    //추가
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            Debug.Log("죽음");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("PrevCol"))
        {

            print("IsMeetBoss TRUE");
            Destroy(collision.gameObject);
            MapManager.instance.OnColBox();
        }
        if (collision.gameObject.CompareTag("AfterCol"))
        {
            isMeetBoss = true;

            Destroy(collision.gameObject);
            MapManager.instance.OffColBox();
            StartCoroutine(CameraFov());


        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            JumpCount = 2;
        }

    }

    IEnumerator CameraFov()
    {
        for (int i = 0; i < 40; i++)
        {
            camera.fieldOfView++;
            yield return new WaitForSeconds(0.01f);
        }
    }
}

