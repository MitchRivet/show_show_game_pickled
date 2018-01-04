using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class ControlFlowManager : MonoBehaviour {

	public static ControlFlowManager Singleton = null;


	public enum STATE
	{
		MASTER,
		INTRO_SCENE,
		RULES_SCENE,
		GAMEPLAY,
		WIN_LOSE_SCREEN,
		END_GAME_BLACK,
		NUM_STATES
	}

	public enum WIN_LOSE_STATE
	{
		WIN,
		LOSE,
		IN_PROGRESS
	}
		
	// Used for level loading
	private STATE _currentState = STATE.INTRO_SCENE;

	public STATE CurrentState
	{
		get
		{
			return _currentState;
		}
		set
		{
			_currentState = value;
		}
	}

	// Used for the final scene to determine if the scene should show losing sprites or winning
	private WIN_LOSE_STATE _currentWinLoseState = WIN_LOSE_STATE.IN_PROGRESS;

	public WIN_LOSE_STATE CurrentWinLoseState
	{
		get
		{
			return _currentWinLoseState;
		}
		set
		{
			_currentWinLoseState = value;
		}
	}

	// value averaged each frame to determine if makey makey had valid input
	private float AvgClicksPerSecThisFrame = 0.0f;
	private float LeftSideAvgClicksPerSecThisFrame = 0.0f;
	private float RightSideAvgClicksPerSecThisFrame = 0.0f;
	// threshold to detmine if makey makey had valid input
	public float CPSThresholdForHit = 2f;

	// can turn off makey makey input
	public bool IsUsingMakeMakeyInput = false;

	private bool LeftSideIsOddNumbers = true;

	public bool IsUsingConfigFile = true;

	private void Awake()
	{
		if(!Singleton)
		{
			Singleton = this;
			DontDestroyOnLoad(Singleton);
		}
		else
		{
			DestroyImmediate(this);
		}
	}


	public float LeftSideAvgCPSThisFrame
	{
		get
		{
			return LeftSideAvgClicksPerSecThisFrame;
		}
	}

	public float RightSideAvgCPSThisFrame
	{
		get
		{
			return RightSideAvgClicksPerSecThisFrame;
		}
	}

	// Use this for initialization
	void Start ()
	{
		
		if (IsUsingConfigFile) {
			Debug.Log ("Application.dataPath:" + Application.dataPath);
			string[] newURL = GetLines ("MMHostURL");
			Debug.Log ("NewURL: " + newURL [0]);
			string[] clicksPerSec = GetLines ("CPS");
			CPSThresholdForHit = float.Parse (clicksPerSec [0]);
			Debug.Log ("clicksPerSec: " + CPSThresholdForHit);
			string[] useMakeyMakeyInput = GetLines ("UseMakeyMakeyInput");
			Debug.Log ("useMakeyMakeyInput: " + useMakeyMakeyInput [0]);

			MMWSServer serverRef = gameObject.GetComponent<MMWSServer> ();
			if (serverRef) {
				if (useMakeyMakeyInput [0].CompareTo ("true") == 0) {
					Debug.Log ("User selected to use makey makey input");
					serverRef.host = newURL [0];
					serverRef.enabled = true;
					IsUsingMakeMakeyInput = true;
					serverRef.InitConnection ();
				} else if (useMakeyMakeyInput [0].CompareTo ("false") == 0) {
					Debug.Log ("User selected not to use makey makey input");
					serverRef.enabled = false;
					IsUsingMakeMakeyInput = false;
				}
			}
		} else {
			if (IsUsingMakeMakeyInput) {
				MMWSServer serverRef = gameObject.GetComponent<MMWSServer> ();
				serverRef.InitConnection();
			}
		}
		SceneManager.LoadScene((int)CurrentState);
	}

	public bool WasMakeyMakeyClickedThisFrame()
	{
		if (AvgClicksPerSecThisFrame > CPSThresholdForHit)
		{
			return true;
		}
		return false;
	}



	private void Update()
	{
		AvgClicksPerSecThisFrame = 0.0f;
		LeftSideAvgClicksPerSecThisFrame = 0.0f;
		RightSideAvgClicksPerSecThisFrame = 0.0f;

		if (IsUsingMakeMakeyInput)
		{
			for (int i = 0; i < MMWSServer.Instance.makeyMakeys.Count; i++)
			{
				//We divide by 30 here because that is the max number of clicks per second in the simulator.
				//In the live verson it won't be locked down
				AvgClicksPerSecThisFrame += MMWSServer.Instance.makeyMakeys[i].cps / 30.0f;

				if( (i % 2) == 1)
				{
					if (LeftSideIsOddNumbers)
					{
						LeftSideAvgClicksPerSecThisFrame += MMWSServer.Instance.makeyMakeys[i].cps;
					}
					else
					{
						RightSideAvgClicksPerSecThisFrame += MMWSServer.Instance.makeyMakeys[i].cps;
					}
				}
				else
				{
					if (LeftSideIsOddNumbers)
					{
						RightSideAvgClicksPerSecThisFrame += MMWSServer.Instance.makeyMakeys[i].cps;
					}
					else
					{
						LeftSideAvgClicksPerSecThisFrame += MMWSServer.Instance.makeyMakeys[i].cps;
					}
				}
			}

			if (MMWSServer.Instance.makeyMakeys.Count > 0)
			{
				AvgClicksPerSecThisFrame /= MMWSServer.Instance.makeyMakeys.Count;
				RightSideAvgClicksPerSecThisFrame = RightSideAvgClicksPerSecThisFrame / (MMWSServer.Instance.makeyMakeys.Count / 2);
				LeftSideAvgClicksPerSecThisFrame = LeftSideAvgClicksPerSecThisFrame / (MMWSServer.Instance.makeyMakeys.Count / 2);
				Debug.Log("LeftSide Avg cps this frame: " + LeftSideAvgClicksPerSecThisFrame);
				Debug.Log("RightSide Avg cps this frame: " + RightSideAvgClicksPerSecThisFrame);
			}
		}
		// Debug.Log("AvgClicksPerSecThisFrame: " + AvgClicksPerSecThisFrame);
		// Debug.Log("Makeymakeycount : " + MMWSServer.Instance.makeyMakeys.Count);
	}

	public void AdvanceScene()
	{
		CurrentState++;
		SceneManager.LoadScene((int)CurrentState);
	}

	public static string[] GetLines(string id)
	{
		ArrayList lines = new ArrayList();
		string line;

		System.IO.FileInfo theSourceFile = null;
		System.IO.TextReader reader = null;  // NOTE: TextReader, superclass of StreamReader and StringReader

		// Read from plain text file if it exists
		theSourceFile = new System.IO.FileInfo(Application.dataPath + "/ShowShowConfig.txt");
		if (theSourceFile != null && theSourceFile.Exists)
		{
			reader = theSourceFile.OpenText();  // returns StreamReader
		}
		else
		{
			// try to read from Resources instead
			//TextAsset puzdata = (TextAsset)Resources.Load("ShowShowConfig", typeof(TextAsset));
			//reader = new System.IO.StringReader(puzdata.text); // returns StringReader
		}

		if (reader == null)
		{
			Debug.Log("puzzles.txt not found or not readable");
			return new string[0];
		}
		else
		{
			string lineID = "[" + id + "]";
			bool match = false;
			// Read each line from the file/resource
			while ((line = reader.ReadLine()) != null)
			{
				if (match)
				{
					if (line.StartsWith("["))
					{
						break;
					}
					if (line.Length > 0)
					{
						lines.Add(line);
					}
				}
				else if (line.StartsWith(lineID))
				{
					match = true;
				}
			}
			reader.Close();
			if (lines.Count > 0)
			{
				return (string[])lines.ToArray(typeof(string));
			}
			return new string[0];
		}
	}
}