using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

	//Timerの変数
	int min = 3;
	float sec = 0;
	int oldSec = 0;
	bool timerFlag = true;
	Text TimerText;

	public GameObject[] player;
	bool CountDown = true;
	Text StartMes;


	//end処理
	GameObject winner;
	Vector3 finPos;




	// Use this for initialization
	void Start () {
		Physics.gravity = new Vector3 (0, -30f, 0);
		TimerText = GameObject.Find("Timer").GetComponent<Text> ();
		StartMes = GameObject.Find("CountDown").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (CountDown) {
			//カウントダウン処理
			bigin ();
		} else {
			if (min != 0 || oldSec != 0) {
				//制限時間のカウント
				if (Time.timeScale > 0) {
					sec -= Time.deltaTime;
					if (sec <= 0) {
						min--;
						sec += 60;
					}

					int a = (int)sec;
					if (a != oldSec) {
						TimerText.text = min.ToString () + ":" + a.ToString ("00");
						oldSec = a;
					}
				}
			} else {
				//エンド処理
				end ();
			}
		}

	}


	void bigin() {
		if (Time.timeScale > 0) {
			sec += Time.deltaTime;

			if (sec >= 6) {
				//時間計測開始
				CountDown = false;
				StartMes.GetComponent<Text> ().enabled = false;
				sec = 0;
				for (int i = 0; i < 4; i++) {
					player [i].GetComponent<test_walk> ().enabled = true;
				}
			} else if (sec > 5) {
				if (sec > 5.75f) {
					StartMes.fontSize -= 12;
				} else {
					StartMes.fontSize = 100;
					StartMes.text = "START";
				}
			} else if (sec > 4) {
				if (sec < 4.05f) {
					StartMes.fontSize = 200;
					StartMes.text = "1";
				}
				StartMes.fontSize -= 4;
			} else if (sec > 3) {
				if (sec < 3.05f) {
					StartMes.fontSize = 200;
					StartMes.text = "2";
				}
				StartMes.fontSize -= 4;
			} else if (sec > 2) {
				if (sec < 2.05f) {
					StartMes.fontSize = 200;
					StartMes.text = "3";
				}
				StartMes.fontSize -= 4;
			}
		}
	}

	//未完成
	void end () {
		sec += Time.deltaTime;
		if (sec > 0 && sec < 1.5f) {
			StartMes.enabled = true;
			StartMes.text = "FINISH";
			StartMes.fontSize = 100;
			for (int i = 0; i < 4; i++) {
				player [i].GetComponent<test_walk> ().enabled = false;
			}
		} else if (sec > 1.8f && sec < 2) {
			winner = player [0];
			for (int i = 1; i < 4; i++) {
				if (winner.GetComponent<status> ().Score < player [i].GetComponent<status> ().Score)
					winner = player [i];

				finPos = winner.transform.position + new Vector3 (0, 1.5f, -3.5f) - transform.position;
			}
		} else if (sec > 2 && sec < 3) {
			GetComponent<Animator> ().enabled = false;
			StartMes.enabled = false;
			transform.position += finPos * Time.deltaTime;
			transform.Rotate (new Vector3 (-60 * Time.deltaTime, 0, 0));
		}
	}
}
