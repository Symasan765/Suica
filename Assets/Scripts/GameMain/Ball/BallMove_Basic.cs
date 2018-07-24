using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove_Basic : Ball_Pure {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Status == EBallStatus.Throw)
        {
            Throw_Straight();
        }
	}

    // km/h -> m/s
    float BallSpeed()
    {
        return Time.deltaTime * Speed * 0.28f;
    }

    // ボール送球時挙動_ストレート
    void Throw_Straight()
    {
        this.transform.position = this.transform.position + (this.transform.forward * BallSpeed());
    }
}
