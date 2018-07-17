using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour
{
	//追跡するオブジェクトの参照を持つ変数。この場合はコントローラー
	private SteamVR_TrackedObject trackedObj;

	//コントローラと当たっているオブジェクト
	private GameObject collidingObject;

	//手に持っているオブジェクト
	private GameObject objectInHand;


	//簡単にコントローラーへの情報へアクセス出来るための取得関数
	//indexの値を使って追跡されているオブジェクトを指定できる。
	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}


	//コントローラがColliderがついているオブジェクトと当たると
	//そのオブジェクトを掴めるようにする
	public void OnTriggerEnter(Collider other)
	{
		SetCollidingObject(other);
	}

	//↑と同じ。バグ予防
	public void OnTriggerStay(Collider other)
	{
		SetCollidingObject(other);
	}

	//オブジェクトと離れていて掴むことが可能なものの参照を消す
	public void OnTriggerExit(Collider other)
	{
		if (!collidingObject)
		{
			return;
		}

		collidingObject = null;
	}

	private void SetCollidingObject(Collider col)
	{
		//常にプレイヤーが手に持っている、または
		//当たっているオブジェクトはrigidbodyがない場合、何もしない
		if (collidingObject || !col.GetComponent<Rigidbody>())
		{
			return;
		}

		//掴むことが可能なオブジェクトとして取得する
		collidingObject = col.gameObject;
	}

	void Update()
	{
		//Triggerを押されたらコントローラと当たっているオブジェクト
		//(Rigidbode)があればオブジェクトを握る関数を呼び出す
		if (Controller.GetHairTriggerDown())
		{
			if (collidingObject)
			{
				GrabObject();
			}
		}

		//Triggerを離して手にオブジェクトがあればオブジェクトが離される
		if (Controller.GetHairTriggerUp())
		{
			if (objectInHand)
			{
				ReleaseObject();
			}
		}
	}

	private void GrabObject()
	{
		//掴むことが可能なオブジェクトを手に持っている変数にコピーして
		//collidingObjectの参照を消す
		objectInHand = collidingObject;
		collidingObject = null;
		// オブジェクトをコントローラに付けるためにジョイントを作り
		//オブジェクトのRigidbodyをジョイントにコピーする
		var joint = AddFixedJoint();
		joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
	}

	private FixedJoint AddFixedJoint()
	{
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;
	}

	private void ReleaseObject()
	{
		if (GetComponent<FixedJoint>())
		{
			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());
			objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
			objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
		}

		objectInHand = null;
	}
}