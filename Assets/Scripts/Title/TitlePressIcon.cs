using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePressIcon : MonoBehaviour {

    //
    public float Duration = 1.0f;
    public Text PressText;

    //
    private float Alpha = 0.0f;
    private int AlphaSign = 1;
    private Color DefaultColor;

	// Use this for initialization
	void Start () {
        DefaultColor = PressText.color;

    }
	
	// Update is called once per frame
	void Update () {

        DefaultColor.a = TextAlpha();
        PressText.color = DefaultColor;
        
	}

    float TextAlpha() {
        Duration = Mathf.Min(1, Duration + Time.deltaTime);
        if (Duration == 1)
        {
            AlphaSign *= -1;
            Duration = 0.0f;
        }
        Alpha += Time.deltaTime * AlphaSign;
        Alpha = Mathf.Clamp01(Alpha);

        return Alpha;
    }
}
