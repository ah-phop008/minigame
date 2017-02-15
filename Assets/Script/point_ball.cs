using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point_ball : MonoBehaviour {

	void OnCollisionEnter (Collision c) {
		if (c.gameObject.CompareTag ("Player")) {
			c.gameObject.GetComponent<status> ().Score += 100;
			Destroy (gameObject);
		}
	}
}
