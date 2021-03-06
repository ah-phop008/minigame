﻿using UnityEngine;
using System.Collections;

public class test_walk : MonoBehaviour {
	
	public float jump = 3;
	public float stump = 10;
	public float hantei_of_jump = 0.7f;
	public float height_of_shot;

	int count = 0;
	int ShotLimitCount = 0;
	bool ShotLimitCount_flg = false;
	bool jump_flg;
	public bool DontMove = false;

	//移動に関する変数
	public float speed = 5;
	float speed2 = 0.1f;
	Vector2 input_value = new Vector2 (1, 1);
	Vector3 direction;
	Vector3 decision_speed = new Vector3(0, 0, 0);
	Vector3 horizontal_move;//キーボードの変数
	public Vector3 center_point;
	Vector3 tmp;

	//その他
	public GameObject Ammo;
	Rigidbody rb;
	status s;
	public int Stump_PlusPoint = 50;
	public int Stumped_MinusPoint = 30;

	Animator anim;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
		s = GetComponent<status> ();
		anim = GetComponent<Animator> ();
	}

	void Update () {
		Stumped_Action ();
		Shoted_Action ();

		Shot ();

		//Debug.Log (Input.GetAxis ("Horizontal") + ", " + Input.GetAxis ("Vertical"));
	}
		
	void FixedUpdate () {
		if (!DontMove) {
			//Move_Keyboard ();
			Move_Gamepad ();
		}
	}



	void OnCollisionEnter(Collision c) {
		if (c.gameObject.CompareTag("Player") && transform.position.y > c.transform.position.y && (c.transform.position.x + hantei_of_jump > transform.position.x) && (c.transform.position.x - hantei_of_jump < transform.position.x) && (c.transform.position.z + hantei_of_jump > transform.position.z) && (c.transform.position.z - hantei_of_jump < transform.position.z)) {
			rb.velocity = new Vector3 (0, stump, 0);
			if (!c.gameObject.GetComponent<status> ().attacked) {
				c.gameObject.GetComponent<status> ().stumped = true;
				c.gameObject.GetComponent<status> ().attacked = true;
				c.gameObject.GetComponent<status> ().Score -= Stumped_MinusPoint;
				GetComponent<status> ().Score += Stump_PlusPoint;
			}
		}

		if (c.gameObject.CompareTag ("Floor")) {
			jump_flg = true;
			DontMove = false;
		}
	}


	void Shot () {
		if (Input.GetButtonDown("Fire") || Input.GetKeyDown (KeyCode.E)) {
			if (ShotLimitCount == 0) {
				GameObject ammo = Instantiate (Ammo);
				ammo.transform.parent = transform;
				ammo.transform.localPosition = Vector3.forward + new Vector3(0, height_of_shot, 0);
				ShotLimitCount_flg = true;
			}
		}
		if (ShotLimitCount_flg) {
			ShotLimitCount++;
			if (ShotLimitCount > 30) {
				ShotLimitCount = 0;
				ShotLimitCount_flg = false;
			}
		}
	}


	void Stumped_Action () {
		if (s.stumped) {
			if (count % 2 == 0) {
				GetComponent<Renderer> ().enabled = false;
			} else {
				GetComponent<Renderer> ().enabled = true;
			}
			count++;

			if (count > 180) {
				s.stumped = false;
				s.attacked = false;
				count = 0;
				GetComponent<Renderer> ().enabled = true;
			}
		}
	}

	void Shoted_Action () {
		if (s.shoted) {
			if (count % 6 < 3) {
				GetComponent<Renderer> ().enabled = false;
			} else {
				GetComponent<Renderer> ().enabled = true;
			}
			count++;

			if (count > 180) {
				s.shoted = false;
				s.attacked = false;
				count = 0;
				GetComponent<Renderer> ().enabled = true;
			}
		}
	}

	void Move_Gamepad () {
		
		input_value = new Vector2(Input.GetAxis ("Horizontal"), Input.GetAxis("Vertical"));

		//キャラの移動
		if (input_value.x != 0 || input_value.y != 0) {
			direction = transform.position - center_point;
			decision_speed.x = input_value.x + (input_value.y / direction.z) * direction.x;
			decision_speed.z = input_value.y;

			rb.MovePosition (transform.position + speed2 * decision_speed);

			//キャラの向き
			transform.forward = decision_speed;

			//animator
			anim.SetBool ("isRunning", true);
		} else {
			anim.SetBool ("isRunning", false);
		}

		//移動の制限
		tmp = transform.position;
		tmp.x = (transform.position.z - center_point.z) / Mathf.Tan(26.2f) - 1;
		tmp.x = Mathf.Clamp (transform.position.x, -tmp.x, tmp.x);
		transform.position = tmp;



		if (Input.GetButtonDown ("Jump") && !s.stumped && jump_flg) {
			rb.AddForce (new Vector3 (0, 5 * jump, 0), ForceMode.Impulse);
			jump_flg = false;
			Debug.Log ("JUMP " + this.transform.name);
		}
	}

	void Move_Keyboard () {
		//移動(キーボード用)
		//rb.MovePosition (transform.position + speed2 * transform.forward);

		if (Input.GetKeyDown (KeyCode.Space) && !s.stumped && jump_flg) {
			rb.AddForce (new Vector3 (0, 5 * jump, 0), ForceMode.Impulse);
			jump_flg = false;
		}

		if (Input.GetKey (KeyCode.W)) {
			transform.rotation = Quaternion.Euler (0, 0, 0);
			rb.MovePosition (transform.position + speed2 * transform.forward);

			float a;
			horizontal_move = transform.position - center_point;
			a = 1 / Mathf.Sqrt (Mathf.Pow(horizontal_move.x, 2) + Mathf.Pow(horizontal_move.z, 2));
			horizontal_move = speed2 * a * horizontal_move;
			rb.MovePosition (transform.position + horizontal_move);
		} else if (Input.GetKey (KeyCode.S)) {
			transform.rotation = Quaternion.Euler (0, 180, 0);
			rb.MovePosition (transform.position + speed2 * transform.forward);

			float a;
			horizontal_move = transform.position - center_point;
			a = 1 / Mathf.Sqrt (Mathf.Pow(horizontal_move.x, 2) + Mathf.Pow(horizontal_move.z, 2));
			horizontal_move = speed2 * a * -1 * horizontal_move;
			rb.MovePosition (transform.position + horizontal_move);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.rotation = Quaternion.Euler (0, 270, 0);
			rb.MovePosition (transform.position + speed2 * transform.forward);
		} else if (Input.GetKey (KeyCode.D)) {
			transform.rotation = Quaternion.Euler (0, 90, 0);
			rb.MovePosition (transform.position + speed2 * transform.forward);
		}
	}
		
}
