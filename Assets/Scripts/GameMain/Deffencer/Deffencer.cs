using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deffencer : MonoBehaviour {

    [Tooltip("走行状態")]
    public bool IsRun = false;
    [Tooltip("自分の持つスクリプト")]
    public HeadLookController headLook;
    [Tooltip("自分のもつアニメーターコントローラ")]
    public Animator animator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LookAtBall();
        // ModeRun();
	}

    void LookAtBall()
    {
        var go = GameObject.FindGameObjectWithTag("Ball");
        if (go)
        {
            //this.transform.LookAt(new Vector3(go.transform.position.x,0, go.transform.position.z));
            headLook.target = go.transform.position;
        }
        else
        {
            this.transform.LookAt(Vector3.zero);
        }
    }

    void ModeRun()
    {
        var go = GameObject.FindGameObjectWithTag("Ball");
        if (go)
        {
            this.transform.LookAt(new Vector3(go.transform.position.x, 0, go.transform.position.z));
        }
        animator.SetBool("Run", IsRun);

    }
}
