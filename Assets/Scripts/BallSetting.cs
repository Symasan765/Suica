using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BallSetting : MonoBehaviour {

    // 球の最大数
    public int maxNum;
    // 現在の球数
    [SerializeField]
    private int currentNum;
    private bool setFlg = false;
    public bool SetFlag
    {
        set { setFlg = value; }
        get { return setFlg; }
    }
    public event Action onSet = null;

    public Canvas canvas;
    public GameObject message;
    public Text text;

    private void Update()
    {
        
        if(text != null)
        {
            text.text = currentNum.ToString();
            if (currentNum < 0)
            {
                text.text = "";
            }
        }
    }

    /// <summary>
    /// 球数を設定
    /// </summary>
    /// <param name="_max">最大数</param>
    public void Set(int _max)
    {
        maxNum = _max;
        currentNum = _max;
        SetFlag = true;
        if(onSet != null)
        {
            onSet();
        }
    }

    /// <summary>
    /// 現在の球数を増やす
    /// </summary>
    /// <param name="_num">増やす数</param>
    /// <returns>球の最大数を超えるとFalseが返る</returns>
    public bool Add(int _num = 1)
    {
        if (currentNum + _num > maxNum)
        {
            return false;
        }

        currentNum += _num;

        return true;
    }

    /// <summary>
    /// 現在の球数を減らす
    /// </summary>
    /// <param name="_num">減らす数</param>
    /// <returns>球が０より下になるとFalseが返る</returns>
    public bool Sub(int _num = 1)
    {
        currentNum -= _num;
        if (currentNum < 0)
        {
            return false;
        }
        
        return true;
    }

    /// <summary>
    /// 現在の球数を最大値にリセットする
    /// </summary>
    public void ReSet()
    {
        currentNum = maxNum;
    }

    public int GetMaxNum() {
        return maxNum;
    }

    public int GetCurrentNum()
    {
        return currentNum;
    }

    public bool IsPitching(int _num = 1)
    {
        if (currentNum < 0) return false ;
        if (!Sub(_num))
        {
            StartCoroutine("enumerator");
            return false;
        }
        return true;
    }

    IEnumerator enumerator()
    {
        Instantiate(message, canvas.transform);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Title");
    }
}
