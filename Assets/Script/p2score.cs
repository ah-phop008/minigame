﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class p2score : MonoBehaviour {

	status s;
	Text score_text;

	// Use this for initialization
	void Start () {
		s = GameObject.Find ("p2").GetComponent<status> ();
		score_text = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		score_text.text = "2P：" + s.Score.ToString();
	}
}
