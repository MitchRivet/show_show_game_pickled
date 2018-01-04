using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplaycontrollerliver : MonoBehaviour {

	public ScalpelPlayerOne playerone = null;
	public ScalpelPlayerTwo playertwo = null; 

	public JarJuiceOne jarjuiceone = null; 
	public JarJuiceTwo jarjuicetwo = null;  

	public JarOne jarone = null; 
	public JarTwo jartwo = null; 

	public bool p1phase1 = false; 
	public bool p2phase2 = false; 

	ControlFlowManager.WIN_LOSE_STATE CheckWinner()
	{
		if(ControlFlowManager.Singleton)
		{
			if (ControlFlowManager.Singleton.IsUsingMakeMakeyInput)
			{
				if (ControlFlowManager.Singleton.LeftSideAvgCPSThisFrame > ControlFlowManager.Singleton.RightSideAvgCPSThisFrame)
				{
					return ControlFlowManager.WIN_LOSE_STATE.WIN;
				}
				else
				{
					return ControlFlowManager.WIN_LOSE_STATE.LOSE;
				}
			}
		}
		return ControlFlowManager.WIN_LOSE_STATE.IN_PROGRESS;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (CheckWinner () == ControlFlowManager.WIN_LOSE_STATE.WIN) {
			playerone.AdvanceOnPath ();

		} else if (CheckWinner () == ControlFlowManager.WIN_LOSE_STATE.LOSE) {

			playertwo.AdvanceOnPath ();
			jarjuicetwo.fillJar2 ();
		} else if (CheckWinner () == ControlFlowManager.WIN_LOSE_STATE.LOSE) {
			Debug.Log ("in player 2"); 
			jarjuicetwo.fillJar2 ();
		} else if (CheckWinner () == ControlFlowManager.WIN_LOSE_STATE.WIN) {
			Debug.Log ("in player 1"); 
			jarjuiceone.fillJar1 (); 
		}
			
	}
}
