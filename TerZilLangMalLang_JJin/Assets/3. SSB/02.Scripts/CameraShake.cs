using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    Transform tempPose;
    private void Awake()
    {
         tempPose = transform;
        instance = this;
    }
    public float shakeTimer = 0; //흔들림 효과 시간
    public float shakeAmount; //흔들림 범위
    Vector3 offset;

    private void Update()
    {
        if (shakeTimer >= 0)
        {
            Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;

            transform.position = transform.position + new Vector3(ShakePos.x, ShakePos.y, 0) + offset;

            shakeTimer -= Time.deltaTime;

            transform.position = tempPose.position;
        }
    }


    public void ShakeCamera(float shakePwr, float shakeDur)
    {
        shakeAmount = shakePwr;
        shakeTimer = shakeDur;
    }

}


