using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JarJuiceOne : MonoBehaviour {
	public bool juiceItOneMoved = false;
	public static bool playerOneVictory = false;




	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (JarOne.jarMoved == true && Input.GetKeyDown ("a") && transform.localScale.y <= 1.0f) {
			Debug.Log ("jar is moved and input is good");
			transform.localScale += new Vector3(0, 0.01F, 0);
		} 

		if (transform.localScale.y >= 1.0f) {
			playerOneVictory = true; 
			Debug.Log ("You Are the Weiner");
		}

		if (playerOneVictory == true) {
			SceneManager.LoadScene("Player_one_wins");
		}
	}

	public void fillJar1() {
		Debug.Log ("filing 1?");
		transform.localScale += new Vector3(0, 0.01F, 0);	
	}
		
}



