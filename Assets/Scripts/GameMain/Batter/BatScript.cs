﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : MonoBehaviour
{
	// バットの先端位置
	Vector3 m_TipNowPos;
	Vector3 m_TipOldPos;

	// バットの終端(手元)位置
	Vector3 m_EndNowPos;
	Vector3 m_EndOldPos;

	GameObject m_Tip;
	GameObject m_End;
	
	public float m_BatMass; // 現在のバットの質量

	private AudioSource audioSource;

	// Use this for initialization
	void Start()
	{
		// バットの先端を取得する
		m_Tip = gameObject.transform.Find("BatTip").gameObject;
		m_End = gameObject.transform.Find("BatEnd").gameObject;

		// 先端位置を取得
		m_TipNowPos = m_Tip.transform.position;
		m_EndNowPos = m_End.transform.position;
		m_TipOldPos = m_TipNowPos;
		m_EndOldPos = m_EndNowPos;

		audioSource = gameObject.GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		m_TipOldPos = m_TipNowPos;
		m_EndOldPos = m_EndNowPos;

		m_TipNowPos = m_Tip.transform.position;
		m_EndNowPos = m_End.transform.position;
	}

	private void OnTriggerEnter(Collider collider)
	{
		// 3D同士が接触した瞬間の１回のみ呼び出される処理
		if (collider.gameObject.tag == "Ball")
		{
			Vector3 cube;
			cube = collider.ClosestPointOnBounds(this.transform.position);
			cube = new Vector3(cube.x, cube.y, transform.position.z);
			Vector3 HitPoint = cube;

			// 当たった位置のバットでの割合を求める
			float sizeX = m_TipNowPos.x - m_EndNowPos.x;
			float t = m_TipNowPos.x - HitPoint.x;
			t = t / sizeX;

			// バット先端、終端のスイングスピードを計算する
			// 移動量を求める。これはFixedDeltaTime辺りの移動量
			Vector3 TipSpeed = m_TipNowPos - m_TipOldPos;
			Vector3 EndSpeed = m_EndNowPos - m_EndOldPos;

			// 単位を秒速に直す
			float rate = 1.0f / Time.deltaTime;    // 一秒間に何回呼ばれるか？
			TipSpeed *= rate;
			EndSpeed *= rate;

			// バットの運動量を求める
			Vector3 SwingSpeedVec = Vector3.Lerp(EndSpeed, TipSpeed, t);
			SwingSpeedVec *= m_BatMass;

			// バットの振った方向を求める
			Vector3 TipVec = TipSpeed.normalized;
			Vector3 EndVec = EndSpeed.normalized;
			Vector3 SwingVec = Vector3.Lerp(EndVec, TipVec, t);

			// 球を飛ばす
			var ridid = collider.gameObject.GetComponent<Rigidbody>();
			Vector3 BallVect = ridid.velocity.normalized;
			Vector3 RayCastPos = HitPoint - (BallVect * 5.0f);

			// レイを飛ばして法線ベクトルを求め反射ベクトルを求める
			RaycastHit hit;
			if (Physics.Raycast(RayCastPos, BallVect, out hit, 15.0f))
			{
				if (hit.collider.gameObject.tag == "Badboy")
				{
					Vector3 normal = hit.normal.normalized;
					float a = Vector3.Dot(BallVect, normal);
					BallVect = BallVect + 2 * a * normal;
					SwingVec += BallVect;
					SwingVec = -SwingVec.normalized;
				}
			}

			ridid.velocity = Vector3.zero;
			ridid.position = HitPoint;
			SwingVec = new Vector3(SwingVec.x, 0.2f, SwingVec.z).normalized;
			ridid.AddForce(SwingVec * SwingSpeedVec.magnitude, ForceMode.Impulse);
			Debug.Log("スイングスピード : " + SwingSpeedVec.magnitude + "kg・m/s");

			// バットの音を鳴らす
			audioSource.Play();
		}
	}
}
