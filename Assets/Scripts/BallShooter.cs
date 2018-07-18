using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour {

    [SerializeField, Tooltip("ボール")]
    public GameObject[] BallList;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // ボールの射出
        if (Input.GetKeyDown(KeyCode.Space))
        {
           GameObject go = Instantiate(BallList[0]);
            go.transform.position = this.transform.position;
            go.transform.rotation = this.transform.rotation;
        }
    }
}
