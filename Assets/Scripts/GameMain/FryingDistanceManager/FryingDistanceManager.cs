using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FryingDistanceManager : MonoBehaviour
{

    [Tooltip("これをベースに距離を測定")]
    public Vector3 BasePoint;
    [Tooltip("ベースとの距離を測るオブジェクト")]
    public GameObject MeasureObj;
    [Tooltip("飛距離")]
    public float FryingDistance;
    [Tooltip("飛距離を表示するテキスト")]
    public Text DistanceText;
    //[Tooltip("テキストの色")]
    //public Color TextColor;
    [Tooltip("True:測定開始 False:測定終了")]
    public bool isCalc;
    [Tooltip("True:y軸含む False:y軸無し")]
    public bool DistanceVec3Flg;

    private float Distance;     // 飛距離計算用

    // Use this for initialization
    void Start()
    {

        // 初期化
        Init();

    }

    // Update is called once per frame
    void Update()
    {

        CheckBallStatus();

        if (isCalc)
        {
            if (DistanceVec3Flg)
            {
                FryingDistance = CalcDistanceVec3();                            // 飛距離計算
                DistanceText.text = FryingDistance.ToString("0.00");        // 小数点以下２桁
            }
            else
            {
                FryingDistance = CalcDistanceVec2();                            // 飛距離計算
                DistanceText.text = FryingDistance.ToString("0.00");        // 小数点以下２桁
            }
        }
    }

    private void Init()
    {
        FryingDistance = 0.0f;
        //DistanceText.color = TextColor;
        DistanceText.text = FryingDistance.ToString("0.00");        // 小数点以下２桁

        isCalc = false;
        DistanceVec3Flg = false;

        // 飛距離計算オブジェクト初期化
        BasePoint = new Vector3(0, 0, 0);
        //MeasureObj = GameObject.FindWithTag("Ball");
    }

    // 距離計算　y軸含む
    private float CalcDistanceVec3()
    {
        Distance = Vector3.Distance(new Vector3(BasePoint.x, BasePoint.y, BasePoint.z), new Vector3(MeasureObj.transform.position.x, MeasureObj.transform.position.y, MeasureObj.transform.position.z));

        if (MeasureObj.transform.position.z < 0)
        {
            Distance = -Distance;
        }

        return Distance;

    }

    // 距離計算 y軸無し
    private float CalcDistanceVec2()
    {
        Distance = Vector2.Distance(new Vector2(BasePoint.x, BasePoint.z), new Vector3(MeasureObj.transform.position.x, MeasureObj.transform.position.z));

        if (MeasureObj.transform.position.z < 0)
        {
            Distance = -Distance;
        }

        return Distance;

    }

    private void CheckBallStatus()
    {
        if (MeasureObj == null)
        {
            MeasureObj = GameObject.FindWithTag("Ball");
        }
        else
        {
            Debug.Log("ボール取得");
            if (MeasureObj.GetComponent<Ball_Pure>().GetBallState() == Ball_Pure.EBallStatus.Flying)
            {
                isCalc = true;
            }
            else
            {
                isCalc = false;
            }
        }

        if (isCalc && MeasureObj.transform.position.y < 0)
        {
            isCalc = false;
        }
    }


    // 計算開始フラグセット
    public void SetIsCalc(bool flg)
    {
        isCalc = flg;

        if (isCalc)
        {
            MeasureObj = GameObject.FindWithTag("Ball");
        }
        else
        {
            MeasureObj = null;
        }
    }
}
