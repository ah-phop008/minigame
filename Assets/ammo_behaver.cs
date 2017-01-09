using UnityEngine;
using System.Collections;

public class ammo_behaver : MonoBehaviour {

	int count;
	Vector3 move_direnction;	//弾の進む方向
	GameObject MyPlayer;	//弾を発射したプレイヤー
	public float speed = 15f;	//弾の速さ
	public int IgnoreGravityTime = 30;	//重力を無視して進む時間

	// Use this for initialization
	void Start () {
		MyPlayer = transform.root.gameObject;
		move_direnction = new Vector3(transform.position.x - MyPlayer.transform.position.x, 0, transform.position.z - MyPlayer.transform.position.z);
		transform.parent = null;
		GetComponent<Rigidbody> ().AddForce (speed * move_direnction, ForceMode.VelocityChange);
		Debug.Log (move_direnction);
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


				//Playerをshotedモードに移行
				s.shoted = true;
				s.attacked = true;
			}
		} else if (c.gameObject.tag == "Target") {
			GameObject target = c.gameObject;
			MyPlayer.GetComponent<status> ().Score += target.GetComponent<target_behaver> ().point;
			Destroy (target);
		}

		if (c.gameObject != MyPlayer.gameObject) {
			Destroy (gameObject);
		}
	}
}
