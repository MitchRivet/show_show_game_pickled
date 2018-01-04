using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_one_wins : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("player_one_wins_path"),"time", 10, "easetype", iTween.EaseType.easeInOutSine));
	}

	// Update is called once per frame
	void Update () {

	}
}
