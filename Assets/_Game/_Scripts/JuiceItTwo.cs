using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceItTwo : MonoBehaviour {
	public bool juiceItTwoMoved = false;
	// Use this for initialization
	void Start () {
		Grow ();
	}

	// Update is called once per frame
	void Update () {

		if (ScalpelPlayerTwo.currentScalpel2PathPercent >= 1.00f && juiceItTwoMoved == false) {
			MoveJuiceIt2 ();
			juiceItTwoMoved = true;
		}

	}

	void Grow() {
		iTween.ScaleBy (gameObject, iTween.Hash("amount", new Vector3 (2.0F, 2.0F, 2.0F),"time", 2.00f, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "Shrink"));
	}

	void Shrink() {
		iTween.ScaleBy (gameObject, iTween.Hash("amount", new Vector3 (0.5F, 0.5F, 0.5F),"time", 2.00f, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "Grow"));
	}


	void MoveJuiceIt2() {
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("juice_it_two"),"time", 2, "easetype", iTween.EaseType.easeInOutSine));
	}
}



