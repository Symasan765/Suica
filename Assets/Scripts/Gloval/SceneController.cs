using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public enum ESceneType
    {
        Title = 0,
        GameMain,
        TYPE_MAX
    }
    [Tooltip("シーンタイプ")]
    public ESceneType CurrentScene = ESceneType.Title;
    public SteamVR_TrackedObject VRTrackedObj;

    // ゲーム中永続
    private void Awake()
    {
        // DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E) || Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            LoadNewScene(ESceneType.GameMain);
        }
	}

    public void LoadNewScene(ESceneType sceneType)
    {
        CurrentScene = sceneType;
        SceneManager.LoadScene(sceneType.ToString());
    }

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)VRTrackedObj.index); }
    }

}
