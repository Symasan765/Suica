using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glove : MonoBehaviour {

    //
    public enum EMitMode
    {
        Aim = 0,    //　狙い
        Trace,      //　捕球
        Catch,      //  とった
        Wait,       // 打ったときとか
        Setup,      // 初期化てきな
        MODE_MAX
    }

    //
    [Tooltip("捕手位置")]
    public GameObject CatchPoint;
    [Tooltip("捕球後の硬直時間")]
    public float StopTimeInCatch = 1.0f;
    [Tooltip("")]
    public AudioSource source;
    [Tooltip("Sound")]
    public AudioClip clip;


    //
    private GameObject BallObject;
    public EMitMode State = EMitMode.Setup;
    private float StackTime = 0;
    private float MoveWeight = 0.2f;
    private Vector3 InitMitPosition = Vector3.zero;


	// Use this for initialization
	void Start () {
        InitMitPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.I))
        {
            State = EMitMode.Setup;
        }
        MitWait();
        MitSetup();
        MitAiming();
        MitTrace();
        MitCatch();
    }

    void MitAiming()
    {
        if (State != EMitMode.Aim) return;

        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position += Vector3.down * MoveWeight;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position += Vector3.up * MoveWeight;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * MoveWeight;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * MoveWeight;
        }

        StackTime = 0;

    }


    void MitTrace()
    {
        if (State != EMitMode.Trace) return;

        Vector3 v3 = BallObject.transform.position;
        v3.z = this.transform.position.z;
        this.transform.position = v3;
    }

    void MitCatch()
    {
        if (State != EMitMode.Catch) return;

        StackTime = Mathf.Min(StopTimeInCatch, StackTime + Time.deltaTime);

        if(StackTime == StopTimeInCatch)
        {
            State = EMitMode.Setup;
        }

    }

    void MitWait()
    {
        if (BallObject)
        {
            if(BallObject.GetComponent<Ball_Pure>().GetBallState() == Ball_Pure.EBallStatus.Flying)
            {
                State = EMitMode.Wait;
            }
        }

        if (State != EMitMode.Wait) return;

        StackTime = Mathf.Min(StopTimeInCatch, StackTime + Time.deltaTime);

        if (StackTime == StopTimeInCatch)
        {
            State = EMitMode.Setup;
        }
    }

    void MitSetup()
    {
        if (State != EMitMode.Setup) return;

        DestroyObject(BallObject);
        this.transform.position = InitMitPosition;

        State = EMitMode.Aim;
    }

    public void SetBallTracing(GameObject ball_)
    {
        BallObject = ball_;
        State = EMitMode.Trace;
    }

    public void SetStateCatch()
    {
        source.PlayOneShot(clip);
        State = EMitMode.Catch;
    }

}
