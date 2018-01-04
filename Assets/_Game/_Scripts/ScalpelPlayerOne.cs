using UnityEngine; 
using System.Collections;



public class ScalpelPlayerOne : MonoBehaviour {
	public static float currentScalpel1PathPercent = 0.0f;

	public static bool scalpel1Leave = false; 


	void Start() {

		//iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("scalpel_1_path"),"time", 5, "easetype", iTween.EaseType.easeInOutSine));
	} 

	void Update() {

		if (Input.GetKeyDown ("a")) { 
			iTween.PutOnPath (gameObject, iTweenPath.GetPath ("scalpel_1_path"), currentScalpel1PathPercent += 0.01f);

		} 

		if (currentScalpel1PathPercent >= 1.00f && scalpel1Leave == false) {
			ScalpelOneLeave ();
			scalpel1Leave = true;	 
		} 

		if (JarOne.scalpel1KillCount <= 0.0f) {
			Destroy (gameObject);
		}
			

	} 

	void ScalpelOneLeave() {
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("scalpel_1_leave"),"time", 3, "easetype", iTween.EaseType.easeInOutSine));
	}


	public void AdvanceOnPath()
	{
		iTween.PutOnPath (gameObject, iTweenPath.GetPath ("scalpel_1_path"), currentScalpel1PathPercent += 0.001f);
	}
}

