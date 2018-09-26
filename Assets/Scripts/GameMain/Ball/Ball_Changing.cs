using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Changing : Ball_Pure
{
    [Tooltip("ボールに力を加えるタイミング")]
    public AnimationCurve EffectiveTiming;
    [Tooltip("最大変化量")]
    public float MaxForce = 0.0f;
    [Tooltip("カーブ方向( 0 : 真上 )")]
    public float CurveAngle = 0.0f;
    [Tooltip("ボールの-Y方向への落ちにくさ")]
    public float BallFloater = 9.0f;


    private Transform Trans;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        Trans = this.transform;
        Trans.Rotate(Vector3.forward, CurveAngle);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Throwing()
    {
        base.Throwing();
        float Dist = Mathf.Max(0, ToBaseDistance - this.transform.position.z);
        Dist = Dist / ToBaseDistance;
        float fPower = MaxForce * EffectiveTiming.Evaluate(Dist) * (Speed / 100.0f);
        Vector3 vPower = Trans.up* fPower;
        RigidbodyComponent.AddForce(Vector3.up * BallFloater, ForceMode.Acceleration);
        RigidbodyComponent.AddForce(vPower);
    }
}
