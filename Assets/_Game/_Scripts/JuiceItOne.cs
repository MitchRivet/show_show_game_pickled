using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceItOne : MonoBehaviour {
	public bool juiceItOneMoved = false;
	public int pulseCount = 0;
	public bool pulse = false; 


	// Use this for initialization
	void Start () {
		Grow ();
	}

	// Update is called once per frame
	void Update () {
		if (pulse == false && juiceItOneMoved == true) {
			
		}

		if (pulse == true && juiceItOneMoved == true) {
			pulseCount += 1;
			transform.localScale -= new Vector3(0.01F, 0.01F, 0.01F);
			if (pulseCount % 5 == 0) {
				pulse = false; 
			}
		}
		if (ScalpelPlayerOne.currentScalpel1PathPercent >= 1.00f && juiceItOneMoved == false) {
			MoveJuiceIt ();
			juiceItOneMoved = true;
		}



	}

	void Grow() {
		iTween.ScaleBy (gameObject, iTween.Hash("amount", new Vector3 (2.0F, 2.0F, 2.0F),"time", 2.00f, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "Shrink"));
	}

	void Shrink() {
		iTween.ScaleBy (gameObject, iTween.Hash("amount", new Vector3 (0.5F, 0.5F, 0.5F),"time", 2.00f, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "Grow"));
	}

	void MoveJuiceIt() {
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("juice_it_one"),"time", 2, "easetype", iTween.EaseType.easeInOutSine));
	}
}



