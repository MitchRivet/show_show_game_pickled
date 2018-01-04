using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickleTwo : MonoBehaviour {
	public bool pickle2Moved = false;
	public static bool pickle2DoneMoving = false; 
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (ScalpelPlayerTwo.currentScalpel2PathPercent >= 1.00f && pickle2Moved == false) {
			MovePickle2 ();
			pickle2Moved = true;
		}

	}

	void MovePickle2() {
		//		iTween.RotateBy(gameObject, iTween.Hash("x", .5, "easeType", "easeInOutBack", "loopType", "pingPong","time", 4 ));
		iTween.RotateBy(gameObject, iTween.Hash("x", 5.0f, "looptype", "pingPong", "time", 6));
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("pickle_2_path"),"time", 4, "easetype", iTween.EaseType.easeInOutSine));
		pickle2DoneMoving = true;

	}
}



