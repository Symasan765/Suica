﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove_Basic : Ball_Pure {

    [Tooltip("ボールの伸び")]
    public float BallFloater = 9.0f;

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}

    // km/h -> m/s
    float BallSpeed()
    {
        return Time.deltaTime * Speed * 0.28f;
    }

    // ボール送球時挙動_ストレート
    protected override void Throwing()
    {
        this.RigidbodyComponent.AddForce(new Vector3(0, 1, 0) * BallFloater, ForceMode.Acceleration);
    }
}
