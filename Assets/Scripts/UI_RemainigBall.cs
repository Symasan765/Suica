using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RemainigBall : MonoBehaviour {

    private int m_imageNum;
    [SerializeField]
    private int m_splitNum;
    [SerializeField]
    private Vector3 m_firstPos;
    [SerializeField]
    private Vector3 m_interval;
    [SerializeField]
    private BallSetting m_ballSetting;
    [SerializeField]
    private GameObject m_prefab;
    [SerializeField]
    private bool m_horiFlg;
    private int m_oldBallNum = 0;



	// Use this for initialization
	void Start () {
		if(m_ballSetting == null)
        {
            Debug.Log("BallSettingがセットされていません。");
        }
        if(m_prefab == null)
        {
            Debug.Log("Prefabがセットされていません。");
        }

        m_ballSetting.onSet += AllRemove;
	}
	
	// Update is called once per frame
	void Update () {
        int currentNum = m_ballSetting.GetCurrentNum();
        
        if (m_oldBallNum != currentNum)
        {

            DrawChange(currentNum);
            m_oldBallNum = currentNum;
        }
	}

    void DrawChange(int _currentNum)
    {
        // 減らす
        if(m_oldBallNum > _currentNum)
        {
            Destroy(gameObject.transform.GetChild(m_oldBallNum-1).gameObject);
            //m_oldBallNum--;
        }

        // 増やす
        if(m_oldBallNum < _currentNum)
        {
            Add(_currentNum);            
        }
    }

    void Add(int _currentNum)
    {
        float x = 0;
        float y = 0;
        // 横向き
        if (m_horiFlg)
        {
            x = m_firstPos.x + (transform.childCount % m_splitNum) * m_interval.x;
            y = m_firstPos.y - (transform.childCount / m_splitNum) * m_interval.y;
        }
        // 縦向き
        else
        {
            x = m_firstPos.x + (_currentNum / m_splitNum) * m_interval.x;
            y = m_firstPos.y - (_currentNum % m_splitNum) * m_interval.y;
        }
  
        // 生成
        GameObject obj = Instantiate(m_prefab,this.transform);
        obj.GetComponent<RectTransform>().position  = new Vector3(x, y, m_firstPos.z);

        m_oldBallNum++;

        // 再起処理
        if(m_oldBallNum != _currentNum)
        {
            Add(_currentNum);
        }
    }

    
    private void AllRemove()
    {
        // 減らす
        for (int i = m_oldBallNum; i > 0; i--)
        {
            Destroy(gameObject.transform.GetChild(m_oldBallNum - 1).gameObject);
            m_oldBallNum--;
        }
    }
}
