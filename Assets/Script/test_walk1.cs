using UnityEngine;
using System.Collections;

public class test_walk1 : MonoBehaviour {
	public float speed = 0.1f;
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

	Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

/*	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown (KeyCode.Space) && jump_flg) {
			rb.AddForce (new Vector3 (0, 5 * jump, 0), ForceMode.Impulse);
			jump_flg = false;
		}

		if (Input.GetKey (KeyCode.F)) {
			rb.MovePosition (transform.position + speed2 * transform.forward);
		}

		if (Input.GetKey (KeyCode.W)) {
			r
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




	Vector3 teisuubai (float a, Vector3 v) {
		v.x *= a; v.y *= a; v.z *= a;
		return v;
	}

*/
}