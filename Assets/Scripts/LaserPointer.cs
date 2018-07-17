using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
	public Transform cameraRigTransform;     // [CameraRig]のtransformコンポーネント
	public Transform headTransform; // プレイヤーの頭(カメラ)のtransformコンポーネント
	public Vector3 teleportReticleOffset; // 的と床が重ならないようにOffsetを設定
	public LayerMask teleportMask;		// テレポート可能なエリアを区別するためのマスク

	//追跡するオブジェクトの参照を持つ変数。この場合はコントローラー
	private SteamVR_TrackedObject trackedObj;

	public GameObject laserPrefab; //レーザーのPrefabへの参照
	private GameObject laser; //レーザーのインスタンス
	private Transform laserTransform; //レーザーのtransform情報

	public GameObject teleportReticlePrefab; // 的のプレハブへの参照
	private GameObject reticle; // 的のインスタンス
	private Transform teleportReticleTransform; // 的のtransformコンポーネント

	private Vector3 hitPoint; //レーザーが当たる点のベクトル情報
	private bool shouldTeleport; // テレポート先がテレポート可能かの判断用

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

	void Start()
	{
		//レーザーのオブジェクトをプレハブから生成する
		laser = Instantiate(laserPrefab);
		//Transformのコンポーネントを最初から取得する
		//(アクセスしやすくするため)
		laserTransform = laser.transform;
		// 的のプレハブから的のオブジェクトを生成する
		reticle = Instantiate(teleportReticlePrefab);
		// 的のTransformコンポーネントを取得する
		teleportReticleTransform = reticle.transform;
	}

	void Update()
	{
		// Touchpadを押されている間、レーザーを表示する
		if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
		{
			RaycastHit hit;

			//コントローラから光線を飛ばす
			//100m以内にオブジェクトと当たったらレーザーを表示する
			if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
			{
				hitPoint = hit.point;

				ShowLaser(hit);

				// 的を表示する
				reticle.SetActive(true);
				// Offsetを当たっている位置に加える
				teleportReticleTransform.position = hitPoint + teleportReticleOffset;
				// テレポートを可能にする
				shouldTeleport = true;
			}
		}
		//Touchpadを離されたらレーザーを非表示にする
		else
		{
			laser.SetActive(false);
			// 的を非表示にする
			reticle.SetActive(false);
		}
		
		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport)
		{
			Teleport();
		}
	}

	private void ShowLaser(RaycastHit hit)
	{
		//レーザーオブジェクトを表示する
		laser.SetActive(true);
		//レーザーと当たっているオブジェクトの位置とコントローラの
		//位置の中心点を求めて、レーザーオブジェクトの位置にする
		laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
		//レーザーオブジェクトを当たっているオブジェクトに向かわせる
		laserTransform.LookAt(hitPoint);
		//レーザーのZ軸の長さをコントローラから当たるオブジェクトまでの距離にする
		laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
			hit.distance); 
	}

	private void Teleport()
	{
		// テレポート中に再テレポートできないようにする
		shouldTeleport = false;
		// 的を消す
		reticle.SetActive(false);
		// CameraRigの位置とプレイヤーの頭の位置の差を求める
		Vector3 difference = cameraRigTransform.position - headTransform.position;
		// 高さの差を消す
		difference.y = 0;
		// テレポート先の位置に差を加える
		// (これがないと微妙に違う位置にテレポートすることがある)
		cameraRigTransform.position = hitPoint + difference;
	}
}

