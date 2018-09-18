using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSystem : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip AudioClip;

    public Text PlayableText;

    // プレイ可能状態
    public bool IsPlayball = false;

	// Use this for initialization
	void Start () {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.PlayOneShot(AudioClip);

        PlayableText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        if( audioSource.isPlaying ==false )
        {
            IsPlayball = true;
            PlayableText.enabled = true;
        }
    }

    public bool GamePlayable()
    {
        return IsPlayball;
    }
}
