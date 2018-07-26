using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchMotioner : MonoBehaviour {

    public enum EPitchState
    {
        Idle,
        Throw,
        STATE_MAX
    }
    [Tooltip("ピッチャーの状態")]
    public EPitchState State = EPitchState.Idle;

    [Tooltip("ピッチャーモデルのアニメーター")]
    public Animator animator;

    public BallShooter shooter;
    public float ShotTimeOffset = 0;
    

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F))
        {
            State = EPitchState.Throw;
        }





        if (State == EPitchState.Idle)
        {

        }
        else if(State == EPitchState.Throw)
        {
            animator.SetBool("Throw", true);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Throw"))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > ShotTimeOffset)
                {
                    State = EPitchState.Idle;
                    animator.SetBool("Throw", false);
                    shooter.ShotBall(0);
                }
            }

        }
	}


}
