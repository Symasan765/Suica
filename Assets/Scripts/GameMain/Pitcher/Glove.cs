using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glove : MonoBehaviour {

    //
    public enum EMitMode
    {
        Aim = 0,    //　狙い
        Trace,      //　捕球
        Catch,     //  とった
        MODE_MAX
    }

    //
    [Tooltip("捕手位置")]
    public GameObject CatchPoint;
    [Tooltip("捕球後の硬直時間")]
    public float StopTimeInCatch = 1.0f;

    //
    private GameObject BallObject;
    public EMitMode State = EMitMode.Aim;
    private float StackTime = 0;
    private float MoveWeight = 0.2f;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
            DestroyObject(BallObject);
            State = EMitMode.Aim;
        }

    }

    public void SetBallTracing(GameObject ball_)
    {
        BallObject = ball_;
        State = EMitMode.Trace;
    }

    public void SetStateCatch()
    {
        State = EMitMode.Catch;
    }
}
