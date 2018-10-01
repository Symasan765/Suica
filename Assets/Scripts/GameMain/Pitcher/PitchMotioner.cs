using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchMotioner : MonoBehaviour {

    private SteamVR_TrackedObject trackedObject;
    public SteamVR_TrackedObject MainControllerObj;

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

    [Tooltip("球種選択 : -1@random")]
    public int BallType = -1;


    private AudioSource audioSource;
    public AudioClip clip;

    public BallSetting ballSetting;
    
    void Awake()
    {
        trackedObject = MainControllerObj;
    }

	// Use this for initialization
	void Start () {
        audioSource = this.GetComponent<AudioSource>();
        //ballSetting.Set(2);
    }
	
	// Update is called once per frame
	void Update () {


        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (trackedObject)
            {
                if (Input.GetKeyDown(KeyCode.F) || Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
                {
                    if (ballSetting.IsPitching())
                    {
                        State = EPitchState.Throw;
                        audioSource.PlayOneShot(clip);
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (ballSetting.IsPitching())
                    {
                        State = EPitchState.Throw;
                        audioSource.PlayOneShot(clip);
                    }
                }
            }

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
                    shooter.ShotBall(BallType);
                }
            }

        }
	}

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObject.index); }
    }

    private void ChangeBallType()
    {
        int typeMax = shooter.GetBallTypeMax();
        if(Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            BallType += 1;
            if(BallType > typeMax)
            {
                BallType = -1;
            }
        }

        BallType = Mathf.Clamp(BallType, -1, typeMax);
    }


}
