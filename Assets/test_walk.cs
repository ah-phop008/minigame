using UnityEngine;
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

	//移動に関する変数
	public float speed = 5;
	float speed2 = 0.1f;
	Vector2 input_value = new Vector2 (1, 1);
	Vector3 direction;
	Vector3 decision_speed = new Vector3(0, 0, 0);
	Vector3 horizontal_move;//キーボードの変数
	public Vector3 center_point;

	//その他
	public GameObject Ammo;
	Rigidbody rb;
	status s;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		s = GetComponent<status> ();
	}

	void Update () {
		Stumped_Action ();
		Shoted_Action ();

		Shot ();

		Debug.Log (Input.GetAxis ("Horizontal") + ", " + Input.GetAxis ("Vertical"));
	}
		
	void FixedUpdate () {

		//Move_Keyboard ();
		Move_Gamepad ();
	}



	void OnCollisionEnter(Collision c) {
		if (c.gameObject.CompareTag("Player") && transform.position.y > c.transform.position.y && (c.transform.position.x + hantei_of_jump > transform.position.x) && (c.transform.position.x - hantei_of_jump < transform.position.x) && (c.transform.position.z + hantei_of_jump > transform.position.z) && (c.transform.position.z - hantei_of_jump < transform.position.z)) {
			rb.velocity = new Vector3 (0, stump, 0);
			if (!c.gameObject.GetComponent<status> ().attacked) {
				c.gameObject.GetComponent<status> ().stumped = true;
				c.gameObject.GetComponent<status> ().attacked = true;
			}
		}

		if (c.gameObject.CompareTag ("Floor")) {
			jump_flg = true;
		}
	}


	void Shot () {
		if (Input.GetKeyDown (KeyCode.E)) {
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

		//キャラの向き
		transform.forward = new Vector3(input_value.x, 0, input_value.y);


		//キャラの移動
		direction = transform.position - center_point;
		decision_speed.x = input_value.x + (input_value.y / Mathf.Pow(direction.z, 2)) * direction.x;  //Mathf.Pow(~~~) → direction.zでもうまくいくかも....
		decision_speed.z = input_value.y;

		rb.MovePosition (transform.position + speed2 * decision_speed);

		if (Input.GetButtonDown ("Jump") && !s.stumped && jump_flg) {
			rb.AddForce (new Vector3 (0, 5 * jump, 0), ForceMode.Impulse);
			jump_flg = false;
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
