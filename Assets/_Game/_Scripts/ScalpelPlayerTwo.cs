using UnityEngine; 
using System.Collections;



public class ScalpelPlayerTwo : MonoBehaviour {
	public static float currentScalpel2PathPercent = 0.0f;

	public static bool scalpel2Leave = false; 

	void Start() {

		//iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("scalpel_1_path"),"time", 5, "easetype", iTween.EaseType.easeInOutSine));
	} 

	void Update() {


		if (Input.GetKeyDown ("l")) { 
			iTween.PutOnPath (gameObject, iTweenPath.GetPath ("scalpel_2_path"), currentScalpel2PathPercent += 0.01f);

		} 

		if (currentScalpel2PathPercent >= 1.00f && scalpel2Leave == false) {
			ScalpelTwoLeave ();
			scalpel2Leave = true;	 
		}

		if (JarTwo.scalpel2KillCount <= 0.0f) {
			Destroy (gameObject);
		}

	} 

	void ScalpelTwoLeave() {
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("scalpel_2_leave"),"time", 3, "easetype", iTween.EaseType.easeInOutSine));
	}

	public void AdvanceOnPath()
	{
		iTween.PutOnPath (gameObject, iTweenPath.GetPath ("scalpel_2_path"), currentScalpel2PathPercent += 0.001f);
	}
}

