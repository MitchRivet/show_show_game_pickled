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

    // threshold to detmine if makey makey had valid input
    public float CPSThresholdForHit = 2f;

    // can turn off makey makey input
    public bool IsUsingMakeMakeyInput = false;

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

    // Use this for initialization
    void Start () {
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
        if (IsUsingMakeMakeyInput)
        {
            for (int i = 0; i < MMWSServer.Instance.makeyMakeys.Count; i++)
            {
                //We divide by 30 here because that is the max number of clicks per second in the simulator.
                //In the live verson it won't be locked down
                AvgClicksPerSecThisFrame += MMWSServer.Instance.makeyMakeys[i].cps / 30.0f;
                Debug.Log("CPS from Makey[" + i + "] = " + MMWSServer.Instance.makeyMakeys[i].cps);
            }

            if (MMWSServer.Instance.makeyMakeys.Count > 0)
                AvgClicksPerSecThisFrame /= MMWSServer.Instance.makeyMakeys.Count;
        }
        // Debug.Log("AvgClicksPerSecThisFrame: " + AvgClicksPerSecThisFrame);
        // Debug.Log("Makeymakeycount : " + MMWSServer.Instance.makeyMakeys.Count);
    }

    public void AdvanceScene()
    {
        CurrentState++;
        SceneManager.LoadScene((int)CurrentState);
    }
}
