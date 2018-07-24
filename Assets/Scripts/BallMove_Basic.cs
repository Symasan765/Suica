using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove_Basic : Ball_Pure {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = this.transform.position + (this.transform.forward * BallSpeed());
	}

    // km/h -> m/s
    float BallSpeed()
    {
        return Time.deltaTime * Speed * 0.28f;
    }
}
