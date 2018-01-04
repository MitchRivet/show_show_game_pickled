using UnityEngine; 
using System.Collections;

public class ManPlayerOne : MonoBehaviour {

	public bool moveManOne = true; 
	public bool manMoved = false;

	void Start() {

	} 

	void Update() {
		if (ScalpelPlayerOne.currentScalpel1PathPercent >= 1.00f && manMoved == false) {
			Debug.Log ("check succeeded");
			MoveMan ();
			manMoved = true;
		}


	}

	void MoveMan() {
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("man_p1_path"),"time", 5, "easetype", iTween.EaseType.easeInOutSine));

	}
}

