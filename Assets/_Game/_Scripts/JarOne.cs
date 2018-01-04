using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarOne : MonoBehaviour {
	public static bool jarMoved = false;
	public static float scalpel1KillCount = 3.0f;
	// Use this for initialization


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (ScalpelPlayerOne.currentScalpel1PathPercent >= 1.00f && jarMoved == false) {
			MoveJar ();
			jarMoved = true;
		}

		if (jarMoved == true && scalpel1KillCount > 0.0f ) {
			scalpel1KillCount -= Time.deltaTime;
		}
		
	}

	void MoveJar() {
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("big_jar_1"),"time", 2, "easetype", iTween.EaseType.easeInOutSine));

	}
}



