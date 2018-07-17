using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveControllerInputTest : MonoBehaviour {

	//追跡するオブジェクトの参照を持つ変数。この場合はコントローラー
	private SteamVR_TrackedObject trackedObj;

	//簡単にコントローラーへの情報へアクセス出来るための取得関数
	//indexの値を使って追跡されているオブジェクトを指定できる。
	private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Update is called once per frame
	void Update () {
		if(Controller.GetAxis() != Vector2.zero) {
			Debug.Log(gameObject.name + Controller.GetAxis());
		}
	}
}
