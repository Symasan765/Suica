using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public GameObject gameObject;
    private BallSetting ballSet;
    private int num;
	// Use this for initialization
	void Start () {
        ballSet = gameObject.GetComponent<BallSetting>();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ballSet.ReSet();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ballSet.Add();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ballSet.Sub();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            num--;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            num++;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ballSet.Set(num);
        }

        Debug.Log("[Ball]最大値" + ballSet.GetMaxNum());
        Debug.Log("[Ball]現在値" + ballSet.GetCurrentNum());
        Debug.Log("現在値" + num);
    }
}
