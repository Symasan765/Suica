using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Pure : MonoBehaviour
{

    //
    public enum EBallStatus
    {
        Init = 0,           // 初期化
        Throw,              // 送球
        Catch,              // 捕球
        Flying,             // 打撃後
        STATUS_MAX
    }

    //
    [Tooltip("球速(k/h)")]
    public float Speed;
    [Tooltip("サウンド")]
    public AudioSource audioSource;
    [Tooltip("球種名")]
    public string BallTypeName = "";

    //
    [Tooltip("ボールの状態")]
    protected EBallStatus Status = EBallStatus.Init;
    [Tooltip("自身のRigidbodyコンポーネント")]
    protected Rigidbody RigidbodyComponent;
    [Tooltip("ホームベーカリーとピッチマウンドとの距離")]
    protected const float ToBaseDistance = 18.44f;


    // Use this for initialization
    protected virtual void Start()
    {
        RigidbodyComponent = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        BallSetup();
        AudioVolumeControll();
        if (Status == EBallStatus.Throw)
        {
            Throwing();
        }
    }

    private void SetBallInGlove(Vector3 newPos)
    {
        if (Status == EBallStatus.Catch)
        {
            this.transform.position = newPos;
        }
    }

    private void BallSetup()
    {
        if (Status != EBallStatus.Init) return;
        RigidbodyComponent.AddForce(this.transform.forward * CalcBallSpeed(), ForceMode.Impulse);
        Status = EBallStatus.Throw;
    }

    // km/h -> m/s
    protected float CalcBallSpeed()
    {
        // return Speed * 0.28f;
        return Speed / 3.6f;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 捕球状態
        if (other.transform.CompareTag("Glove"))
        {
            if (Status == EBallStatus.Throw)
            {
                Status = EBallStatus.Catch;
                SetBallInGlove(other.GetComponent<Glove>().CatchPoint.transform.position);
                this.GetComponent<Rigidbody>().useGravity = false;
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                other.GetComponent<Glove>().SetStateCatch();
            }
        }

        if (other.transform.CompareTag("Badboy"))
        {
            if (Status == EBallStatus.Throw)
            {
                Status = EBallStatus.Flying;
                this.GetComponent<Rigidbody>().useGravity = true;

                var vel = other.GetComponent<Rigidbody>().velocity;
                this.GetComponent<Rigidbody>().AddForce(vel, ForceMode.Force);

            }
        }
    }

    public EBallStatus GetBallState()
    {
        return Status;
    }

    private void AudioVolumeControll()
    {

        audioSource.volume = Mathf.Clamp01(this.RigidbodyComponent.velocity.magnitude / 100.0f);
    }

    // 送球状態にある球の制御
    protected virtual void Throwing()
    {


    }
}