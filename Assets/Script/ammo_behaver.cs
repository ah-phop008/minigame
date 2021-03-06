﻿using UnityEngine;
using System.Collections;

public class ammo_behaver : MonoBehaviour {

	int count;
	Vector3 move_direnction;	//弾の進む方向
	GameObject MyPlayer;	//弾を発射したプレイヤー
	public float speed = 15f;	//弾の速さ
	public int IgnoreGravityTime = 30;	//重力を無視して進む時間
	float BlowOffPower = 3.5f;

	Vector3 SubDirection;
	public Vector3 center_point;

	public int minus_point;
	public int plus_point;

	// Use this for initialization
	void Start () {
		//飛ばす方向の計算
		MyPlayer = transform.root.gameObject;
		move_direnction = new Vector3(transform.position.x - MyPlayer.transform.position.x, 0, transform.position.z - MyPlayer.transform.position.z);
		move_direnction.Normalize ();
		transform.parent = null;
		//縦方向の補正計算
		SubDirection = transform.position - center_point;
		SubDirection.y = 0;
		SubDirection.Normalize ();
		move_direnction.x += SubDirection.x;

		GetComponent<Rigidbody> ().AddForce (speed * move_direnction, ForceMode.VelocityChange);

	}
	
	// Update is called once per frame
	void Update () {
		if (count > IgnoreGravityTime) {
			GetComponent<Rigidbody> ().useGravity = true;
		}
		count++;
	}


	void OnCollisionEnter (Collision c) {
		if (c.gameObject.tag == "Player" && c.gameObject != MyPlayer.gameObject) {
			GameObject p = c.gameObject;
			status s = p.GetComponent<status> ();
			if (!s.attacked) {
				//プレイヤーを吹き飛ばす
				move_direnction.y = 2;
				p.GetComponent<Rigidbody> ().AddForce (BlowOffPower * move_direnction, ForceMode.VelocityChange);
				p.GetComponent<test_walk> ().DontMove = true;
				p.GetComponent<status> ().Score -= minus_point;
				MyPlayer.GetComponent<status> ().Score += plus_point;
				//Playerをshotedモードに移行
				s.shoted = true;
				s.attacked = true;
			}
		}

		if (c.gameObject != MyPlayer.gameObject) {
			Destroy (gameObject);
		}
	}
}
