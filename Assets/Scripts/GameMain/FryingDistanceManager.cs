using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FryingDistanceManager : MonoBehaviour {

    [Tooltip("これをベースに距離を測定")]
    public Transform BasePoint;
    [Tooltip("ベースとの距離を測るオブジェクト")]
    public GameObject MeasureObj;
    [Tooltip("飛距離")]
    public float FryingDistance;
    [Tooltip("飛距離を表示するテキスト")]
    public Text DistanceText;
    [Tooltip("True:測定開始 False:測定終了")]
    public bool isCalc;

    private float Distance;

	// Use this for initialization
	void Start () {

        // 初期化
        Init();

	}
	
	// Update is called once per frame
	void Update () {

        if (isCalc)
        {
            FryingDistance = CalcDistance();                            // 飛距離計算
            DistanceText.text = FryingDistance.ToString("0.00");        // 小数点以下２桁
        }

	}

    private void Init()
    {
        FryingDistance = 0.0f;
        DistanceText.text = FryingDistance.ToString("0.00");        // 小数点以下２桁

        isCalc = false;
    }

    // 距離計算
    private float CalcDistance()
    {
        Distance = Vector3.Distance(new Vector3(BasePoint.position.x, BasePoint.position.y, BasePoint.position.z), new Vector3(MeasureObj.transform.position.x, MeasureObj.transform.position.y, MeasureObj.transform.position.z));

        if(MeasureObj.transform.position.z < 0)
        {
            Distance = -Distance;
        }

        return Distance;
        
    }

}
