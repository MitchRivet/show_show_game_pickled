using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinsNice : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Grow ();
	}

	// Update is called once per frame
	void Update () {

	}

	void Grow() {
		iTween.ScaleBy (gameObject, iTween.Hash("amount", new Vector3 (2.0F, 2.0F, 2.0F),"time", 2.00f, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "Shrink"));
	}

	void Shrink() {
		iTween.ScaleBy (gameObject, iTween.Hash("amount", new Vector3 (0.5F, 0.5F, 0.5F),"time", 2.00f, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "Grow"));
	}

}
