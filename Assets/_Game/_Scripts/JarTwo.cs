using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarTwo : MonoBehaviour {
	public static bool jarTwoMoved = false;
	// Use this for initialization
	public static float scalpel2KillCount = 3.0f;
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (ScalpelPlayerTwo.currentScalpel2PathPercent >= 1.00f && jarTwoMoved == false) {
			MoveJar2 ();
			jarTwoMoved = true;
		}

		if (jarTwoMoved == true && scalpel2KillCount > 0.0f ) {
			scalpel2KillCount -= Time.deltaTime;
		}

	}

	void MoveJar2() {
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("big_jar_2"),"time", 2, "easetype", iTween.EaseType.easeInOutSine));

	}
}



