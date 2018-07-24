using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour {

    [SerializeField, Tooltip("ボール")]
    public GameObject[] BallList;
    [Tooltip("レイの長さ")]
    public float RayLength = 18.44f;
    [Tooltip("レイ可視化用オブジェクト(円柱)")]
    public GameObject RayMesh;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ShotBall();
        AimRay();
    }

    // 射出方向へのレイ
    void AimRay()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if(Physics.SphereCast(ray, BallList[0].transform.localScale.x, out hit, 20.0f))
        {
            RayView(hit.distance);

        }
    }

    // ボールの射出
    void ShotBall()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject go = Instantiate(BallList[0]);
            go.transform.position = this.transform.position;
            go.transform.rotation = this.transform.rotation;
        }
    }

    // レイ可視化メッシュの変形
    void RayView(float dist)
    {
        // pos
        RayMesh.transform.localPosition = new Vector3(0, 0, dist / 2.0f);
        // length
        RayMesh.transform.localScale = new Vector3(0.1f, dist / 2.0f, 0.1f);
    }
}
