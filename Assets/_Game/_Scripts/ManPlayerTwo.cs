using UnityEngine; 
using System.Collections;

public class ManPlayerTwo : MonoBehaviour {

	public bool moveManTwo = true; 
	public bool manMovedTwo = false;

	void Start() {
		

	} 

	void Update() {
		if (ScalpelPlayerTwo.currentScalpel2PathPercent >= 1.00f && manMovedTwo == false) {
			Debug.Log ("check succeeded");
			MoveMan2 ();
			manMovedTwo = true;
		}


	}

	void MoveMan2() {
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("man_p2_path"),"time", 5, "easetype", iTween.EaseType.easeInOutSine));

	}
}

