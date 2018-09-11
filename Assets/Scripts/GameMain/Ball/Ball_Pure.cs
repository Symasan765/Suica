using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Pure : MonoBehaviour {

    public enum EBallStatus
    {
        Throw = 0,          // 送球
        Catch,              // 捕球
        Flying,             // 打撃後
        STATUS_MAX
    }

    [Tooltip("球速(k/h)")]
    public float Speed;
    [Tooltip("ボールの状態")]
    public EBallStatus Status = EBallStatus.Throw;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetBallInGlove(Vector3 newPos)
    {
        if(Status == EBallStatus.Catch)
        {
            this.transform.position = newPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
                // 捕球状態
        if (other.transform.CompareTag("Glove"))
        {
            if(Status == EBallStatus.Throw)
            {
                Status = EBallStatus.Catch;
                SetBallInGlove(other.GetComponent<Glove>().CatchPoint.transform.position);
            }
        }

        if(other.transform.CompareTag("Badboy"))
        {
            if(Status == EBallStatus.Throw)
            {
                Status = EBallStatus.Flying;
                this.GetComponent<Rigidbody>().useGravity = true;

                var vel = other.GetComponent<Rigidbody>().velocity;
                this.GetComponent<Rigidbody>().AddForce(vel, ForceMode.Force);

            }
        }
    }
}
