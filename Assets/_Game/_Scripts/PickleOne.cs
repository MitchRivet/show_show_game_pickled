using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickleOne : MonoBehaviour {
	public bool pickleMoved = false;
	public static bool pickleDoneMoving = false; 
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (ScalpelPlayerOne.currentScalpel1PathPercent >= 1.00f && pickleMoved == false) {
			MovePickle ();
			pickleMoved = true;
		}

	}

	void MovePickle() {
//		iTween.RotateBy(gameObject, iTween.Hash("x", .5, "easeType", "easeInOutBack", "loopType", "pingPong","time", 4 ));
		iTween.RotateBy(gameObject, iTween.Hash("x", 5.0f, "looptype", "pingPong", "time", 6));
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("pickle_1_path"),"time", 4, "easetype", iTween.EaseType.easeInOutSine));
		pickleDoneMoving = true;

	}
}



