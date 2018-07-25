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
    [Tooltip("ミットオブジェクト")]
    public GameObject GloveObject;
    [Tooltip("球速(k/h)")]
    public float ShotSpeed = 100.0f;
    [Tooltip("管理しているボール")]
    private GameObject ManageBall;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        LookAtGlove();
        ShotBall();
        RayView(Vector3.Distance(this.transform.position, GloveObject.transform.position));

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
            if (ManageBall)
            {
                DestroyObject(ManageBall);
            }

            ManageBall = Instantiate(BallList[0]);
            ManageBall.transform.position = this.transform.position;
            ManageBall.transform.rotation = this.transform.rotation;
            ManageBall.GetComponent<Ball_Pure>().Speed = ShotSpeed;     //球速のセット
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

    // ミットへ向く
    void LookAtGlove()
    {
        this.transform.LookAt(GloveObject.transform);
    }
}
