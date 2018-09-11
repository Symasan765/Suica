using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoulLineControll : MonoBehaviour {

    [Tooltip("ポール右")]
    public GameObject PoleRight;
    [Tooltip("ポール左")]
    public GameObject PoleLeft;
    [Tooltip("ライン右")]
    public GameObject LineRight;
    [Tooltip("ライン左")]
    public GameObject LineLeft;
    [Tooltip("ホームベース")]
    public GameObject HomeBase;


    [Tooltip("ポールの位置を自動設定")]
    public bool IsAutoSetPole = false;
    [Tooltip("ラインの長さ")]
    public float LineLength = 130.0f;
    private Vector3 oldLengthR = Vector3.zero;
    private Vector3 oldLengthL = Vector3.zero;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PoleAutoSet();
        MakeLine();
	}

    void MakeLine()
    {
        if (oldLengthL == PoleLeft.transform.position && oldLengthR == PoleRight.transform.position) return;

        float dist = 0.0f;  // ポールまでの距離
        Vector3 newpos = Vector3.zero;
        // Right
        dist = Vector3.Distance(HomeBase.transform.position, new Vector3(PoleRight.transform.position.x, 0.0f, PoleRight.transform.position.z));
        LineRight.transform.localScale = new Vector3(0.05f, 0.1f, dist);
        LineRight.transform.LookAt(new Vector3(PoleRight.transform.position.x, -0.045f, PoleRight.transform.position.z));
        newpos = HomeBase.transform.position;
        newpos += LineRight.transform.forward * dist / 2.0f;
        newpos.y = -0.045f;
        LineRight.transform.position = newpos;
        // Left
        dist = Vector3.Distance(HomeBase.transform.position, new Vector3(PoleLeft.transform.position.x, 0.0f, PoleLeft.transform.position.z));
        LineLeft.transform.localScale = new Vector3(0.05f, 0.1f, dist);
        LineLeft.transform.LookAt(new Vector3(PoleLeft.transform.position.x, -0.045f, PoleLeft.transform.position.z));
        newpos = HomeBase.transform.position;
        newpos += LineLeft.transform.forward * dist / 2.0f;
        newpos.y = -0.045f;
        LineLeft.transform.position = newpos;

        oldLengthL = PoleLeft.transform.position;
        oldLengthR = PoleRight.transform.position;
    }

    void PoleAutoSet()
    {
        if (!IsAutoSetPole) return;
        PoleLeft.transform.position = new Vector3(-LineLength, 10.0f, LineLength);
        PoleRight.transform.position = new Vector3(LineLength, 10.0f, LineLength);
    }


}
