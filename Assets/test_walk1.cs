using UnityEngine;
using System.Collections;

public class test_walk1 : MonoBehaviour {
	public float speed;
	float a;
	Vector3 horizontal_move;
	public Vector3 center_point;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.RightControl)) {
			GetComponent<Rigidbody> ().AddForce (new Vector3 (0, 15, 0), ForceMode.Impulse);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			horizontal_move = transform.position - center_point;
			horizontal_move.y = 0;
			a = 1 / Mathf.Sqrt (Mathf.Pow(horizontal_move.x, 2) + Mathf.Pow(horizontal_move.y, 2) + Mathf.Pow(horizontal_move.z, 2));
			transform.Translate (teisuubai(speed * a, horizontal_move));
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			horizontal_move = transform.position - center_point;
			horizontal_move.y = 0;
			a = 1 / Mathf.Sqrt (Mathf.Pow(horizontal_move.x, 2) + Mathf.Pow(horizontal_move.y, 2) + Mathf.Pow(horizontal_move.z, 2));
			transform.Translate (teisuubai(speed * a * -1, horizontal_move));
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate (new Vector3 (-1 * speed, 0, 0));
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate (new Vector3 (speed, 0, 0));
		}

	}




	Vector3 teisuubai (float a, Vector3 v) {
		v.x *= a; v.y *= a; v.z *= a;
		return v;
	}
}
