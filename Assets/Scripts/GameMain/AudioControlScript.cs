using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControlScript : MonoBehaviour {
    AudioSource m_AudioSource;
    public float m_DelaySec;

    // Use this for initialization
    void Start () {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.time = m_DelaySec;
        m_AudioSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
