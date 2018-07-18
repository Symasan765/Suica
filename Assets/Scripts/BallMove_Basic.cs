using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove_Basic : MonoBehaviour {

    public float Speed = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = this.transform.position + (this.transform.forward * Time.deltaTime * Speed);
	}
}
