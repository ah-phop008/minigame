using UnityEngine;
using System.Collections;

public class test_walk : MonoBehaviour {
	public float speed = 5;
	public float jump = 3;
	public float stump = 10;
	public float hantei_of_jump = 0.7f;
	public float height_of_shot;

	int count = 0;
	int ShotLimitCount = 0;
	bool ShotLimitCount_flg = false;
	bool jump_flg;



	Vector3 horizontal_move;
	public Vector3 center_point;

	public GameObject Ammo;

	Rigidbody rb;
	float NowSpeedOf_y;
	float NowSpeedOf_z;

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
	}
		
	void FixedUpdate () {

		//移動

		if (Input.GetKeyDown (KeyCode.Space) && !s.stumped && jump_flg) {
			rb.AddForce (new Vector3 (0, 5 * jump, 0), ForceMode.Impulse);
			jump_flg = false;
		}

		if (Input.GetKey (KeyCode.W)) {
			float a;
			NowSpeedOf_y = rb.velocity.y;
			horizontal_move = transform.position - center_point;
			a = 1 / Mathf.Sqrt (Mathf.Pow(horizontal_move.x, 2) + Mathf.Pow(horizontal_move.z, 2));
			horizontal_move = (teisuubai(speed * a, horizontal_move));
			horizontal_move.y = NowSpeedOf_y;
			rb.velocity = horizontal_move;
		} else if (Input.GetKey (KeyCode.S)) {
			float a;
			NowSpeedOf_y = rb.velocity.y;
			horizontal_move = transform.position - center_point;
			a = 1 / Mathf.Sqrt (Mathf.Pow(horizontal_move.x, 2) + Mathf.Pow(horizontal_move.z, 2));
			horizontal_move = (teisuubai(speed * a * -1, horizontal_move));
			horizontal_move.y = NowSpeedOf_y;
			rb.velocity = horizontal_move;
		}
		if (Input.GetKey (KeyCode.A)) {
			NowSpeedOf_y = rb.velocity.y;
			NowSpeedOf_z = rb.velocity.z;
			rb.velocity = (new Vector3 (-1 * speed, NowSpeedOf_y, NowSpeedOf_z));
		} else if (Input.GetKey (KeyCode.D)) {
			NowSpeedOf_y = rb.velocity.y;
			NowSpeedOf_z = rb.velocity.z;
			rb.velocity = (new Vector3 (speed, NowSpeedOf_y, NowSpeedOf_z));
		}
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


	Vector3 teisuubai (float a, Vector3 v) {
		v.x *= a; v.y *= a; v.z *= a;
		return v;
	}
}
