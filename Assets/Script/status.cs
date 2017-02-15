using UnityEngine;
using System.Collections;

public class status : MonoBehaviour {

	public bool stumped = false;
	public bool shoted = false;
	public bool attacked = false;
	public int Score = 0;

	void Update () {
		if (Score < 0) Score = 0;
	}
}
