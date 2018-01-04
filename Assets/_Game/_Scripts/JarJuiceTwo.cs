using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JarJuiceTwo : MonoBehaviour {
	public bool juiceItTwoMoved = false;
	public static bool playerTwoVictory = false;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (JarTwo.jarTwoMoved == true && Input.GetKeyDown ("l") && transform.localScale.y <= 1.0f) {
			Debug.Log ("jar is moved and input is good");
			transform.localScale += new Vector3(0, 0.01F, 0);
		} 

		if (transform.localScale.y >= 1.0f) {
			playerTwoVictory = true; 
			Debug.Log ("You Are the Weiner");
		}

		if (playerTwoVictory == true) {
			SceneManager.LoadScene("player_two_wins");
		}
	}

	public void fillJar2() {
		
			transform.localScale += new Vector3(0, 0.01F, 0);
	
	}

}



