using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove_Basic : MonoBehaviour {

    [Tooltip("球速(km/h)")]
    public float Speed = 100.0f;

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
