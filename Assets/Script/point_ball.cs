using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point_ball : MonoBehaviour {
	public int point;

	void OnCollisionEnter (Collision c) {
		if (c.gameObject.CompareTag ("Player")) {
			c.gameObject.GetComponent<status> ().Score += point;
			Destroy (gameObject);
		}
	}
}
